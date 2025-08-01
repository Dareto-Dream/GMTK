using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using System.Threading.Tasks;

public class DialogueUI : MonoBehaviour
{
    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialoguePanel;
    public float textSpeed = 0.02f;

    private DialogueLine[] lines;
    private int index = 0;
    private bool isTyping = false;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = new PlayerInput();

        var clickAction = playerInput.currentActionMap["Click"];

        clickAction.started += ctx => NextLine();
        
    }

    private void Onable()
    {
        playerInput.enabled = true;
    }

    private void OnDisable()
    {
        playerInput.enabled = false;
    }

    public void StartDialogue(DialogueScript script)
    {
        lines = script.lines;
        index = 0;
        dialoguePanel.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        speakerNameText.text = lines[index].speakerName;
        dialogueText.text = "";

        foreach (char c in lines[index].text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
    }

    public void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = lines[index].text;
            isTyping = false;
            return;
        }

        index++;
        if (index < lines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }
}
