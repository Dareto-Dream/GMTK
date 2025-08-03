using UnityEngine;

public class GuardLogic : MonoBehaviour
{
    [SerializeField] private DialogueScript script;
    private DialogueUI dialogueUI;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        dialogueUI = GameObject.FindGameObjectWithTag("DialogueSystem").GetComponent<DialogueUI>();
        dialogueUI.StartDialogue(script, GameManager.Instance.EndLoop);
    }
}
