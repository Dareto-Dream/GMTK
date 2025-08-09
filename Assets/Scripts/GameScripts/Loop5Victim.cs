using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Loop5Victim : MonoBehaviour
{
    [SerializeField] DialogueUI dialogueUI;
    [SerializeField] DialogueScript scriptLoop5;
    [SerializeField] GameObject extraPanel;
    [Header("Choice UI")]
    [SerializeField] GameObject choicePanel;
    [SerializeField] Button shootButton;
    [SerializeField] Button waitButton;

    [SerializeField] Button continueButton;

    [Header("Ending Display")]
    [SerializeField] GameObject endingPanel; // Panel to hold the ending text, optional
    [SerializeField] TextMeshProUGUI endingText;       // Assign a UI Text or TMP_Text

    [SerializeField] GameObject screenFader;

    private PlayerController player;
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;
        if (collision.CompareTag("Player") && GameManager.Instance.IsLoop(5))
        {
            //Debug.Log("THIS IS SPARTA");
            triggered = true;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            player = collision.GetComponent<PlayerController>();
            dialogueUI.StartDialogue(scriptLoop5, TheEnd);
        }
    }

    private void TheEnd()
    {
        player.ChangeCurrentInput("UI");

        Debug.Log("THe end");
        choicePanel.SetActive(true);
        screenFader.SetActive(false);
        shootButton.onClick.RemoveAllListeners();
        waitButton.onClick.RemoveAllListeners();

        player.Freeze();

        shootButton.onClick.AddListener(HandleShoot);
        waitButton.onClick.AddListener(HandleWait);
    }

    private void HandleShoot()
    {
        choicePanel.SetActive(false);
        ShowEndingText("The shot rings out. The target drops. For a moment, you see your own face staring back at you in confusion.");
    }

    private void HandleWait()
    {
        choicePanel.SetActive(false);
        ShowEndingText("The target’s eyes narrow. In a flash, they draw and fire. You drop. The last thing you see is yourself, weapon shaking.");
    }

    private void ShowEndingText(string text)
    {
        if (endingPanel != null) endingPanel.SetActive(true);
        if (endingText != null)
            endingText.text = text + "\n\n(Click to continue...)";
        if (continueButton != null)
        {
            continueButton.gameObject.SetActive(true);
            continueButton.onClick.RemoveAllListeners();
            continueButton.onClick.AddListener(EndGame);
        }
        else
        {
            Debug.Log("Fuck you");
            // If no button, just end instantly
            EndGame();
        }
    }

    private System.Collections.IEnumerator WaitForClickThenEnd()
    {
        bool clicked = false;
        while (!clicked)
        {
            if (Input.GetMouseButtonDown(0))
                clicked = true;
            yield return null;
        }
        EndGame();
    }

    private void EndGame()
{
    if (continueButton != null)
        continueButton.gameObject.SetActive(false);
    SceneManager.LoadScene("Credits");
}

}
