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

    private Vector3 _Movement;
    private Rigidbody2D _Rigidbody;
    private PlayerController playerController;
    private GameObject bird;

    [SerializeField] private GameObject sniperRifleAim;
    [SerializeField] private MapController mapController;
    [SerializeField] private Sprite[] birdAnimation;

    public DialogueScript script;
    private DialogueUI dialogueUI;

    private float SPEED_MULTIPLIER = 5f;

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

        dialogueUI.StartDialogue(script);
        Debug.LogWarning("Started dialogue!");
    }

    private IEnumerator ShootingMagic()
    {
        foreach (Sprite sprite in birdAnimation)
        {
            yield return new WaitForSeconds(0f);
            bird.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
        }
        StopSniping();
    }

    private void FixedUpdate()
    {
        if (!is_sniping) return;
        _Movement = playerController.GetMovement();
        _Rigidbody.linearVelocity = _Movement * SPEED_MULTIPLIER; // sprint or walk
        if (playerController.GetMouseButton() && !is_shooting)
        {
            StartCoroutine(ShootingMagic());
            is_shooting = true;
        }
    }
}
