using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    Vector2 _Movement;
    Rigidbody2D _Rigidbody;


    //idk how to call it but okay, vars
    private PlayerInput playerInput;
    private CharacterAnimationLogic characterAnimationLogic;
    [SerializeField] private DialogueScript scriptLoop4;
    [SerializeField] private DialogueScript scriptLoop5;
    [SerializeField] private DialogueUI dialogueUI;

    //bools
    private bool is_sprinting = false;
    private bool is_freezing = false;
    private bool is_mouse_down = false;


    

    //constants
    private float SPEED_MULTIPLIER = 4f; // Change to 4f
    private float SPRINTING_SPEED_MULTIPLIER = 25f; //Change to 6f

    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        var mouseAction = playerInput.currentActionMap["Mouse"];
        var sprintAction = playerInput.currentActionMap["Sprint"];
        sprintAction.started += ctx => OnSprintStart();
        sprintAction.canceled += ctx => OnSprintStop();
        mouseAction.started += ctx => OnMouseDown();
        mouseAction.canceled += ctx => OnMouseUp();

        characterAnimationLogic = GetComponent<CharacterAnimationLogic>();
    }

    public void LoopReset()
    {
        is_freezing = false;
        is_freezing = false;
        is_mouse_down = false;

        transform.position = new Vector3(-4.5f, 1f, -1f);

        if (GameManager.Instance.IsLoop(4))
        {
            dialogueUI.StartDialogue(scriptLoop4);
        }
        
    }

    public void Freeze()
    {
        is_freezing = true;
    }

    public void Unfreeze()
    {
        is_freezing = false;
    }

    public void Hide()
    {
        GetComponent<SpriteRenderer>().enabled = false;   
    }

    public void Unhide()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void ChangePosition(Vector3 pos)
    {
        if (pos.x == -999)
            pos.x = transform.position.x;
        transform.position = pos;
    }

    private void OnMove(InputValue value)
    {
        _Movement = value.Get<Vector2>();
    }

    public Vector3 GetMovement()
    {
        return _Movement;
    }

    public bool GetMouseButton()
    {
        return is_mouse_down;
    }

    private void OnInteract(InputValue value)
    {
        Debug.Log("Interactiong with surrounded objects. . .");
        InteractWithNearby();
    }

    private void OnSprintStart()
    {
        is_sprinting = true;
    }

    private void OnSprintStop()
    {
        is_sprinting = false;
    }

    private void OnMouseDown()
    {
        is_mouse_down = true;
    }

    private void OnMouseUp()
    {
        is_mouse_down = false;
    }

    private void InteractWithNearby()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.05f);
        foreach (var hit in hits)
        {
            var interactable = hit.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact();
                break;
            }
        }
    }


    private void FixedUpdate()
    {
        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;
        Vector2 direction = (mouseWorldPos - transform.position).normalized;
        float angle = Mathf.Atan2(direction.x, -direction.y) * Mathf.Rad2Deg + 180f;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        if (is_freezing)
        {
            _Rigidbody.linearVelocity = Vector2.zero;
            return;
        }
        _Rigidbody.linearVelocity = is_sprinting ? _Movement * SPRINTING_SPEED_MULTIPLIER : _Movement * SPEED_MULTIPLIER;
        if (_Movement.x != 0f || _Movement.y != 0f)
        {
            characterAnimationLogic.StartAnimation();
            AudioHandler.Instance.PlayUI(AudioHandler.Instance.footsteps);
        }
        else
        {
            characterAnimationLogic.StopAnimation();
            AudioHandler.Instance.StopUI();
        }

    }
}
