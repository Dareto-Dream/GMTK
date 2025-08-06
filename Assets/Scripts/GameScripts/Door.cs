using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public int targetMapIndex;
    public MapController mapController;
    public Vector3 playerPosition;

    private Collider2D col;


    public void Interact()
    {
        AudioHandler.Instance.PlaySFX(AudioHandler.Instance.doorOpen);
        mapController.Blacken();
        col.GetComponent<PlayerController>().ChangePosition(playerPosition);
        mapController.ChangeMap(targetMapIndex);
        AudioHandler.Instance.PlaySFX(AudioHandler.Instance.doorClose);
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            col = collision;
        }
    }
}
