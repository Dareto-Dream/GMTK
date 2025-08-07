using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Door : MonoBehaviour, IInteractable
{
    public int targetMapIndex;
    public MapController mapController;
    public Vector3 playerPosition;

    private Collider2D col;


    public void Interact()
    {
        StartCoroutine(Interecting());
    }

    private IEnumerator Interecting()
    {
        AudioHandler.Instance.PlaySFX(AudioHandler.Instance.doorOpen);
        mapController.Blacken();
        yield return new WaitForSeconds(0.5f);
        col.GetComponent<PlayerController>().ChangePosition(playerPosition);
        mapController.ChangeMap(targetMapIndex);
        AudioHandler.Instance.PlaySFX(AudioHandler.Instance.doorClose);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            col = collision;
        }
    }
}
