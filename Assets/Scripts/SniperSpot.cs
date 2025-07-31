using System.Collections;
using UnityEngine;

public class SniperSpot : MonoBehaviour, IInteractable
{

    [SerializeField] private Sprite sniperRifle;
    [SerializeField] private Vector3 playerPos;

    private bool is_set_up = false;

    private PlayerController playerController;

    public void Interact()
    {
        if (!is_set_up)
        {
            playerController.ChangePosition(playerPos);
            playerController.Freeze();
            StartCoroutine(SetSniperRifle());
        }
    }

    IEnumerator SetSniperRifle()
    {
        yield return new WaitForSeconds(3f);

        GetComponent<SpriteRenderer>().sprite = sniperRifle;
        Debug.Log("Sniper is set up, ready to shoot!");
    }


    private void OggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerController = collision.GetComponent<PlayerController>();
        }
    }
}
