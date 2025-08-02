using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SniperHandler : MonoBehaviour
{

    private bool is_sniping = false;
    private bool is_shooting = false;
    private bool try_to_find_object = false;

    private Vector3 _Movement;
    private Rigidbody2D _Rigidbody;
    private PlayerController playerController;
    private GameObject bird;

    [SerializeField] private GameObject sniperRifleAim;
    [SerializeField] private MapController mapController;
    [SerializeField] private Sprite[] birdAnimation;
    [SerializeField] private Sprite emptySprite;

    public DialogueScript scriptLoop1;
    public DialogueScript scriptLoop2_1;
    public DialogueScript scriptLoop2_2;
    public DialogueScript scriptLoop4;
    private DialogueUI dialogueUI;

    private float SPEED_MULTIPLIER = 5f;

    [HideInInspector] public int shootingAttempts = 0;
    private int shootingAttemptsMax = 3;

    [HideInInspector] public bool is_loop_1 = false;
    [HideInInspector] public bool is_loop_2 = false;
    [HideInInspector] public bool is_loop_4 = false;
    [HideInInspector] public bool is_able_to_shoot = true;

    public void ResetIsDone()
    {
        try_to_find_object = true;
    }

    public void LoopReset()
    {
        is_loop_1 = false;
        is_loop_4 = false;
        is_shooting = false;
        is_sniping = false;
        if (is_loop_2) is_able_to_shoot = false;
        try_to_find_object = false;
        shootingAttempts = 0;

        bird.GetComponent<UnityEngine.UI.Image>().sprite = emptySprite;
    }

    private void Awake()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
        _Rigidbody = GetComponent<Rigidbody2D>();
        bird = sniperRifleAim.transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        dialogueUI = FindFirstObjectByType<DialogueUI>();
    }

    public void StartSniping()
    {

        _Rigidbody.simulated = true;
        is_sniping = true;
    }

    public void StopSniping()
    {
        is_sniping = false;
        transform.localPosition = new Vector3(0f, 0f, -10f);
        playerController.Unhide();
        playerController.Unfreeze();
        sniperRifleAim.SetActive(false);
        mapController.ChangeMap(6);
        _Rigidbody.simulated = false;

        if (is_loop_1)
        {
            dialogueUI.StartDialogue(scriptLoop1, GameManager.Instance.EndLoop);
        }
        else if (is_loop_2)
        {
            if (!is_able_to_shoot)
                dialogueUI.StartDialogue(scriptLoop2_1);
            else
            {
                dialogueUI.StartDialogue(scriptLoop2_2, GameManager.Instance.EndLoop);
            }
        }
        else if (is_loop_4)
        {
            dialogueUI.StartDialogue(scriptLoop4);
        }
    }

    private IEnumerator ShootingMagic()
    {
        if (is_loop_1)
        {
            // NOTE: add shooting sounds
            foreach (Sprite sprite in birdAnimation)
            {
                yield return new WaitForSeconds(0.02f);
                bird.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
            }
            StopSniping();

        }
        else if (is_loop_2)
        {
            if (!is_able_to_shoot)
            {
                StopSniping();
            }
            else
            {
                yield return new WaitForSeconds(2f);
                StopSniping();
            }
            
        }
        else if (is_loop_4)
        {

        }
    }

    private void FixedUpdate()
    {
        if (try_to_find_object)
        {
            if (GameObject.FindGameObjectWithTag("SniperSpot") != null)
            {
                GameObject.FindGameObjectWithTag("SniperSpot").GetComponent<SniperSpot>().is_done = false;
                try_to_find_object = false;
                is_shooting = false;
            }
        }
        if (!is_sniping) return;
        _Movement = playerController.GetMovement();
        _Rigidbody.linearVelocity = _Movement * SPEED_MULTIPLIER; // sprint or walk
        if (playerController.GetMouseButton() && !is_shooting)
        {
            Debug.Log("Pressed LMB " + shootingAttempts + " " + is_shooting + " " + is_able_to_shoot);
            if (is_loop_2 && shootingAttempts < shootingAttemptsMax)
            {
                shootingAttempts++;
                Debug.Log("SHOOTING! " + shootingAttempts);
            }
            else
            {
                StartCoroutine(ShootingMagic());
                is_shooting = true;
            }
        }
    }
}
