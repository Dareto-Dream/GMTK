using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public int targetMapIndex;
    public MapController mapController;
    public Vector3 playerPosition;

    private Collider2D col;


    public void Interact()
    {
        col.GetComponent<PlayerController>().ChangePosition(playerPosition);
        mapController.ChangeMap(targetMapIndex);
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            col = collision;
        }
    }
}
