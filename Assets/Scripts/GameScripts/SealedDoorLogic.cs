using UnityEngine;

public class SealedDoorLogic : MonoBehaviour, IInteractable
{

    [SerializeField] private MapController mapController;

    [SerializeField] private DialogueUI dialogueUI;
    [SerializeField] private DialogueScript script;

    private void Awake()
    {
        if (GameManager.Instance.IsLoop(5))
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void Interact()
    {
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player.Freeze();
        mapController.ChangeMap(7);
        dialogueUI.StartDialogue(script,player.Unfreeze);
    }
}
