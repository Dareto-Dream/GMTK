using System.Collections;
using UnityEngine;

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
    [SerializeField] private GameObject NPC;

    [SerializeField] private Sprite brokenSniperSprite;

    public DialogueScript scriptLoop1;
    public DialogueScript scriptLoop2_1;
    public DialogueScript scriptLoop2_2;
    public DialogueScript scriptLoop4;
    private DialogueUI dialogueUI;


    private float SPEED_MULTIPLIER = 5f;

    [HideInInspector] public int shootingAttempts = 0;
    private int shootingAttemptsMax = 3;

    [HideInInspector] public bool is_able_to_shoot = true;

    public void ResetIsDone()
    {
        try_to_find_object = true;
    }

    public void LoopReset()
    {
        is_shooting = false;
        is_sniping = false;
        if (GameManager.Instance.IsLoop(2)) is_able_to_shoot = false;
        try_to_find_object = false;
        shootingAttempts = 0;

        bird.GetComponent<UnityEngine.UI.Image>().sprite = emptySprite;
    }

    private void Awake()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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

        if (GameManager.Instance.IsLoop(1))
        {
            dialogueUI.StartDialogue(scriptLoop1, GameManager.Instance.EndLoop);
        }
        else if (GameManager.Instance.IsLoop(2))
        {
            if (!is_able_to_shoot)
                dialogueUI.StartDialogue(scriptLoop2_1);
            else
            {
                dialogueUI.StartDialogue(scriptLoop2_2, GameManager.Instance.EndLoop);
            }
        }
        else if (GameManager.Instance.IsLoop(4))
        {
            dialogueUI.StartDialogue(scriptLoop4, GameManager.Instance.EndLoop);
        }
    }

    private IEnumerator ShootingMagic()
    {
        if (GameManager.Instance.IsLoop(1))
        {
            // NOTE: add shooting sounds
            foreach (Sprite sprite in birdAnimation)
            {
                yield return new WaitForSeconds(0.02f);
                bird.GetComponent<UnityEngine.UI.Image>().sprite = sprite;
            }
            StopSniping();

        }
        else if (GameManager.Instance.IsLoop(2))
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
        else if (GameManager.Instance.IsLoop(4))
        {
            Debug.Log("Works correctly!");
            try_to_find_object = true;
            StopSniping();
        }
    }

    private void FixedUpdate()
    {
        if (try_to_find_object)
        {
            if (GameObject.FindGameObjectWithTag("SniperSpot") != null && GameManager.Instance.IsLoop(2))
            {
                AudioHandler.Instance.PlayMusic(AudioHandler.Instance.mainTheme);
                GameObject.FindGameObjectWithTag("SniperSpot").GetComponent<SniperSpot>().is_done = false;
                try_to_find_object = false;
                is_shooting = false;
                NPC.SetActive(false);
            }
            else if (GameObject.FindGameObjectWithTag("SniperSpot") != null && GameManager.Instance.IsLoop(4))
            {
                GameObject.FindGameObjectWithTag("SniperSpot").GetComponent<SpriteRenderer>().sprite = brokenSniperSprite;
                try_to_find_object = false;
            }
        }
        if (!is_sniping)
        {
            AudioHandler.Instance.PlayMusic(AudioHandler.Instance.mainTheme);
            Vector3 some = playerController.gameObject.transform.position;
            transform.position = new Vector3(some.x, some.y, -10);
            return;
        }
        _Movement = playerController.GetMovement();
        _Rigidbody.linearVelocity = _Movement * SPEED_MULTIPLIER; // sprint or walk
        if (playerController.GetMouseButton() && !is_shooting)
        {
            Debug.Log("Pressed LMB " + shootingAttempts + " " + is_shooting + " " + is_able_to_shoot);
            if (GameManager.Instance.IsLoop(2) && shootingAttempts < shootingAttemptsMax)
            {
                shootingAttempts++;
                Debug.Log("SHOOTING! " + shootingAttempts);
            }
            else
            {
                if(GameManager.Instance.IsLoop(1)) AudioHandler.Instance.PlaySFX(AudioHandler.Instance.shoot);
                StartCoroutine(ShootingMagic());
                is_shooting = true;
            }
        }
    }
}
