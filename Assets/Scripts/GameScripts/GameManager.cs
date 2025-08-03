using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int loopCount = 2;

    [SerializeField] private SniperHandler sniperHandler;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private MapController mapController;
    [SerializeField] private SniperSpot sniperSpot;
    [SerializeField] private ProblemSpawner problemSpawner;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        StartNewLoop();
    }



    public void StartNewLoop()
    {
        loopCount++;
        // Reset world state here
        Debug.Log($"Loop {loopCount} started!");

        // RESET everything!
        sniperHandler.LoopReset();
        playerController.LoopReset();
        sniperSpot.LoopReset();

        switch (loopCount)
        {
            // First case: missing the shot because of a bird
            // Second case: lost the ammo, and when returned everyone left the stage
            // Third case: Guards are waiting for him
            // Fourth case: He sets up a weapon in the wrong way and it explodes
            // Fifth case: He kills victim and dies, or victim kills him.


            case 1:
                sniperHandler.is_loop_1 = true;
                mapController.ChangeMap(0);
                break;
            case 2:
                sniperHandler.is_loop_2 = true;
                problemSpawner.is_loop_2 = true;
                sniperHandler.LoopReset();
                mapController.ChangeMap(0);
                break;
            case 3:
                problemSpawner.is_loop_3 = true;
                mapController.ChangeMap(0);
                break;
            case 4:
                sniperHandler.is_loop_4 = true;
                mapController.ChangeMap(0);
                break;
            case 5:
                break;
        }
    }

    public void EndLoop()
    {
        Debug.Log($"Loop {loopCount} ended!");
        if (loopCount == 6) return;
        StartNewLoop();
    }
}