using UnityEngine;

public class ProblemSpawner : MonoBehaviour
{
    private GameObject droppedItem;
    private Vector3 positionOfDroppedItem = new Vector3(-2.5f, 7.5f, -1f);

    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private GameObject guardPrefab;
    [SerializeField] private LampLogic lampLogic;

    private bool spawnedGuard = false;
    private bool droppedAmmo = false;

    private GameObject guard;

    private void Awake()
    {
        if (GameManager.Instance.IsLoop(3) && !spawnedGuard)
        {
            SpawnGuard();
        }
        else if (GameManager.Instance.IsLoop(5) && spawnedGuard)
        {
            Destroy(guard);
        }
    }

    private void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnLoopChanged += OnLoopChanged;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnLoopChanged -= OnLoopChanged;
    }

    private void OnLoopChanged(int loop)
    {
        // Reset spawn trackers each loop
        spawnedGuard = false;
        droppedAmmo = false;
    }

    private void DropAmmo()
    {
        droppedItem = Instantiate(droppedItemPrefab, positionOfDroppedItem, Quaternion.identity, transform.parent);
        lampLogic.LightsOut();
        droppedAmmo = true;
    }

    private void SpawnGuard()
    {
        guard = Instantiate(guardPrefab, transform);
        spawnedGuard = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only drop ammo in loop 2, and only once per loop
        if (GameManager.Instance.IsLoop(2) && !droppedAmmo)
        {
            DropAmmo();
        }
    }
}
