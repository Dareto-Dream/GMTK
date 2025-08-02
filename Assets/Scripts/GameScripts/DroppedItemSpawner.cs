using UnityEngine;

public class DroppedItemSpawner : MonoBehaviour
{
    private GameObject droppedItem;
    private Vector3 positionOfDroppedItem = new Vector3(-2.5f, 7.5f, -1f);

    [SerializeField] private GameObject droppedItemPrefab;

    [HideInInspector] public bool is_loop_2 = false;

    private void DropAmmo()
    {
        droppedItem = Instantiate(droppedItemPrefab, positionOfDroppedItem, Quaternion.identity, transform.parent);
        Debug.Log("Some light stuff. . .");
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (is_loop_2) {
            DropAmmo();
        }
    }
}
