using UnityEngine;

public class Loop5Victim : MonoBehaviour
{

    [SerializeField] DialogueUI dialogueUI;
    [SerializeField] DialogueScript scriptLoop5;


    private void TheEnd()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && GameManager.Instance.IsLoop(5))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f) ;
            dialogueUI.StartDialogue(scriptLoop5, TheEnd);
        }
    }
}
