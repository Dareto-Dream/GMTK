using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int loopCount = 4; // Start at 0 for full play, 3 for mid-jam testing

    [SerializeField] private SniperHandler sniperHandler;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private MapController mapController;
    [SerializeField] private SniperSpot sniperSpot;
    [SerializeField] private ProblemSpawner problemSpawner;
    [SerializeField] private BarrierLogic barrierLogic;
    [SerializeField] private SealedDoorLogic sealedDoorLogic;

    public static GameManager Instance { get; private set; }

    // Centralized loop state
    public int CurrentLoop => loopCount;
    public bool IsLoop(int n) => loopCount == n;

    // Event for loop change
    public event Action<int> OnLoopChanged;

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
        AudioHandler.Instance.PlayMusic(AudioHandler.Instance.mainTheme);
        loopCount++;
        Debug.Log($"Loop {loopCount} started!");

        // RESET everything!
        sniperHandler.LoopReset();
        if (loopCount < 4)
        {
            playerController.LoopReset();
        }
        sniperSpot.LoopReset();

        // Notify all listeners that the loop changed
        OnLoopChanged?.Invoke(loopCount);

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
                mapController.ChangeMap(0);
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