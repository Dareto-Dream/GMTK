using UnityEngine;

public class ProblemSpawner : MonoBehaviour
{
    private GameObject droppedItem;
    private Vector3 positionOfDroppedItem = new Vector3(-2.5f, 7.5f, -1f);

    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private GameObject guardPrefab;

    [HideInInspector] public bool is_loop_2 = false;
    [HideInInspector] public bool is_loop_3 = false;

    private bool is_spawned = false;

    private GameObject guard;

    private void DropAmmo()
    {
        droppedItem = Instantiate(droppedItemPrefab, positionOfDroppedItem, Quaternion.identity, transform.parent);
        Debug.Log("Some light stuff. . .");
    }

    private void Update()
    {
        if (is_loop_3 && !is_spawned)
        {
            guard = Instantiate(guardPrefab, transform);
            is_spawned = true;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (is_loop_2)
        {
            DropAmmo();
            is_loop_2 = false;
        }
    }
}
