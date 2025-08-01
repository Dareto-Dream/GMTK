using UnityEngine;

public class PositionChanger : MonoBehaviour
{
    public int targetMapIndex;
    public MapController mapController;
    public Vector3 playerPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().ChangePosition(playerPosition);
            mapController.ChangeMap(targetMapIndex);
        }
    }
}