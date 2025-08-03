using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int loopCount = 3; // Set to 0 if you want to start at loop 1 in a real build

    [SerializeField] private SniperHandler sniperHandler;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private MapController mapController;
    [SerializeField] private SniperSpot sniperSpot;
    [SerializeField] private ProblemSpawner problemSpawner;
    [SerializeField] private BarrierLogic barrierLogic;

    public static GameManager Instance { get; private set; }

    // Centralized loop state
    public int CurrentLoop => loopCount;
    public bool IsLoop(int n) => loopCount == n;

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
        Debug.Log($"Loop {loopCount} started!");

        // RESET everything!
        sniperHandler.LoopReset();
        if (loopCount < 4)
        {
            playerController.LoopReset();
        }
        sniperSpot.LoopReset();

        // Loop-specific logic
        switch (loopCount)
        {
            case 1:
                mapController.ChangeMap(0);
                break;
            case 2:
                mapController.ChangeMap(0);
                break;
            case 3:
                mapController.ChangeMap(0);
                break;
            case 4:
                mapController.ChangeMap(4);
                playerController.LoopReset();
                break;
            case 5:
                playerController.LoopReset();
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