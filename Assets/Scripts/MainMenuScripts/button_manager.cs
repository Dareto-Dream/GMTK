using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ButtonManager : MonoBehaviour
{
    [Header("Assign the Options Canvas in Inspector")]
    public GameObject optionsCanvas;
    public string GameScene;

    [Header("Splash Animation")]
    public Image splashBgImage;       // Assign in inspector
    public Image gifImage;            // Assign in inspector
    public string bgResourceName = "splash_bg"; // Without extension, must be in Resources/
    public string gifFramePrefix = "Gif/Loading/Untitled_Artwork-";
    public int gifStart = 1;
    public int gifEnd = 29;
    public float frameDelay = 0.1f;

    // Called when Start button is pressed
    public void StartGame()
    {
        AudioHandler.Instance.PlayUI(AudioHandler.Instance.UIClick);

        StartCoroutine(PlaySplashThenLoad());
    }

    private IEnumerator PlaySplashThenLoad()
    {
        splashBgImage.gameObject.SetActive(true);
        gifImage.gameObject.SetActive(true);
        // Set RectTransform to fill canvas
        RectTransform bgRect = splashBgImage.GetComponent<RectTransform>();
        bgRect.anchorMin = Vector2.zero;
        bgRect.anchorMax = Vector2.one;
        bgRect.offsetMin = Vector2.zero;
        bgRect.offsetMax = Vector2.zero;

        RectTransform gifRect = gifImage.GetComponent<RectTransform>();
        gifRect.anchorMin = Vector2.zero;
        gifRect.anchorMax = Vector2.one;
        gifRect.offsetMin = Vector2.zero;
        gifRect.offsetMax = Vector2.zero;
        // Load and show background PNG (if you have one, else comment this out)
        Sprite bgSprite = Resources.Load<Sprite>(bgResourceName);
        if (bgSprite && splashBgImage)
        {
            splashBgImage.sprite = bgSprite;
            splashBgImage.gameObject.SetActive(true);
        }

        // Load GIF frames
        List<Sprite> frames = new List<Sprite>();
        for (int i = gifStart; i <= gifEnd; i++)
        {
            string path = gifFramePrefix + i;
            Sprite frame = Resources.Load<Sprite>(path);
            if (frame != null)
                frames.Add(frame);
            else
                Debug.LogWarning($"Frame not found: {path}");
        }

        // Show and animate GIF
        if (gifImage)
            gifImage.gameObject.SetActive(true);
        foreach (Sprite frame in frames)
        {
            gifImage.sprite = frame;
            yield return new WaitForSeconds(frameDelay);
        }

        // Hide splash UI (optional)
        if (splashBgImage) splashBgImage.gameObject.SetActive(false);
        if (gifImage) gifImage.gameObject.SetActive(false);

        // Load the scene
        SceneManager.LoadScene(GameScene);
    }

    // Called when Options button is pressed
    public void ShowOptions()
    {
        AudioHandler.Instance.PlayUI(AudioHandler.Instance.UIClick);

        optionsCanvas?.SetActive(true);
    }

    public void HideOptions()
    {
        AudioHandler.Instance.PlayUI(AudioHandler.Instance.UIClick);

        optionsCanvas?.SetActive(false);
    }

    // Called when Exit button is pressed
    public void ExitGame()
    {
        AudioHandler.Instance.PlayUI(AudioHandler.Instance.UIClick);

        Debug.Log("Exiting game...");
        Application.Quit();

        // For editor testing
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
