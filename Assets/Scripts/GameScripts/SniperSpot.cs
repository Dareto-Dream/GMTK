using System.Collections;
using UnityEngine;

public class SniperSpot : MonoBehaviour, IInteractable
{

    [SerializeField] private Sprite sniperRifle;
    [SerializeField] private Vector3 playerPos;

    public MapController mapController;

    private bool is_set_up = false;

    private PlayerController playerController;

    public void Interact()
    {
        Debug.Log("Test completed");
        if (!is_set_up)
        {
            playerController.ChangePosition(playerPos);
            playerController.Freeze();
            StartCoroutine(SetSniperRifle());
        }
        else
        {
            playerController.Hide();
            mapController.ChangeMap(7);
        }
    }

    IEnumerator SetSniperRifle()
    {
        yield return new WaitForSeconds(3f);

        GetComponent<SpriteRenderer>().sprite = sniperRifle;
        Debug.Log("Sniper is set up, ready to shoot!");
        is_set_up = true;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerController>();
        }
    }
}
