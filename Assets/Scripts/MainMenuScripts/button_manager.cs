using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [Header("Assign the Options Canvas in Inspector")]
    public GameObject optionsCanvas;
    public string GameScene;

    // Called when Start button is pressed
    public void StartGame()
    {
        // Change "GameScene" to your actual scene name
        SceneManager.LoadScene(GameScene);
    }

    // Called when Options button is pressed
    public void ShowOptions()
    {
        optionsCanvas?.SetActive(true);
    }

    public void HideOptions()
    {
        optionsCanvas?.SetActive(false);
    }

    // Called when Exit button is pressed
    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();

        // For editor testing
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
