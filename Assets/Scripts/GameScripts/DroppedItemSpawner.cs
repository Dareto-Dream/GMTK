using UnityEngine;

public class ProblemSpawner : MonoBehaviour
{
    private GameObject droppedItem;
    private Vector3 positionOfDroppedItem = new Vector3(-2.5f, 7.5f, -1f);

    [SerializeField] private GameObject droppedItemPrefab;
    [SerializeField] private GameObject guard;

    [HideInInspector] public bool is_loop_2 = false;
    [HideInInspector] public bool is_loop_3 = false;

    private void DropAmmo()
    {
        droppedItem = Instantiate(droppedItemPrefab, positionOfDroppedItem, Quaternion.identity, transform.parent);
        Debug.Log("Some light stuff. . .");
    }

    private void Update()
    {
        if (is_loop_3)
        {
            Instantiate(guard, transform);
            is_loop_3 = false;
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
