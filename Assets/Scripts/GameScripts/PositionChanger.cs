using UnityEngine;

public class PositionChanger : MonoBehaviour
{
    public int targetMapIndex;
    public MapController mapController;
    public Vector3 playerPosition;

    [HideInInspector] public bool is_loop_2 = false;

    [SerializeField] private GameObject positionChanger;
    [SerializeField] private GameObject droppedItemPrefab;
    private GameObject droppedItem;
    private Vector3 positionOfDroppedItem = new Vector3();

    private void DropAmmo()
    {
        droppedItem = Instantiate(droppedItemPrefab, positionOfDroppedItem, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (is_loop_2 && positionChanger == this.gameObject) {
            DropAmmo();
        }
        else if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().ChangePosition(playerPosition);
            mapController.ChangeMap(targetMapIndex);
        }
    }
}