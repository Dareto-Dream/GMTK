using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loop5Victim : MonoBehaviour
{
    [SerializeField] DialogueUI dialogueUI;
    [SerializeField] DialogueScript scriptLoop5;
    [Header("Choice UI")]
    [SerializeField] GameObject choicePanel;
    [SerializeField] Button shootButton;
    [SerializeField] Button waitButton;
    [Header("Ending Display")]
    [SerializeField] GameObject endingPanel; // Panel to hold the ending text, optional
    [SerializeField] Text endingText;        // Assign a UI Text or TMP_Text

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;
        if (collision.CompareTag("Player") && GameManager.Instance.IsLoop(5))
        {
            Debug.Log("THIS IS SPARTA");
            triggered = true;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            dialogueUI.StartDialogue(scriptLoop5, TheEnd);
        }
    }

    private void TheEnd()
    {
        choicePanel.SetActive(true);
        shootButton.onClick.RemoveAllListeners();
        waitButton.onClick.RemoveAllListeners();

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
        {
            endingText.text = text + "\n\n(Click anywhere to continue...)";
            StartCoroutine(WaitForClickThenEnd());
        }
        else
        {
            // If no text assigned, just end instantly
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
        SceneManager.LoadScene("Credits"); // Or Application.Quit()
    }
}
