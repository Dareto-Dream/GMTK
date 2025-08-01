using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;
using UnityEngine.InputSystem.UI;

public class DialogueUI : MonoBehaviour
{ 
    public TextMeshProUGUI speakerNameText;
    public TextMeshProUGUI dialogueText;
    public Image characterFace;
    public GameObject dialoguePanel;
    public float textSpeed = 0.02f;

    private DialogueLine[] lines;
    private int index = 0;
    private bool is_typing = false;
    private InputAction clickAction;
    private Action<InputAction.CallbackContext> clickCallback;
    private InputSystemUIInputModule uiInputModule;

    public PlayerInput playerInput;
    
    

    private void Awake()
    {

        var UIMap = playerInput.actions.FindActionMap("UI");
        if (UIMap == null) Debug.LogError("Can't find UI map!");
        clickAction = UIMap?.FindAction("Click");
        if (clickAction == null) Debug.LogError("Can't find Click action!");

        var eventSystem = EventSystem.current;
        if (eventSystem != null)
            uiInputModule = eventSystem.GetComponent<InputSystemUIInputModule>();


        clickCallback = ctx => NextLine();
        clickAction.performed += clickCallback;

        playerInput.SwitchCurrentActionMap("Player");
    }

    public void StartDialogue(DialogueScript script)
    {

        if (uiInputModule != null)
            uiInputModule.enabled = false;

        playerInput.SwitchCurrentActionMap("UI");
        lines = script.lines;
        index = 0;
        dialoguePanel.SetActive(true);

        if (clickCallback == null) clickCallback = ctx => NextLine();
        if (clickAction != null) clickAction.performed += clickCallback;

        StartCoroutine(TypeLine());
        Debug.LogWarning("Started coroutine!");
    }

    public void EndDialogue()
    {
        if (clickAction != null && clickCallback != null)
        clickAction.performed -= clickCallback;

        if (uiInputModule != null)
            uiInputModule.enabled = true;

        lines = null;
        dialoguePanel.SetActive(false);
        playerInput.SwitchCurrentActionMap("Player");
        Debug.LogWarning("End of Dialogie!");
    }

    IEnumerator TypeLine()
    {
        is_typing = true;
        speakerNameText.text = lines[index].speakerName;
        characterFace.sprite = lines[index].characterFace;
        dialogueText.text = "";

        foreach (char c in lines[index].text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        is_typing = false;
    }

    public void NextLine()
    {

        if (lines == null || !dialoguePanel.activeSelf) return;

        if (is_typing)
        {
            StopAllCoroutines();
            dialogueText.text = lines[index].text;
            is_typing = false;
            return;
        }

        index++;
        if (index < lines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }
}
