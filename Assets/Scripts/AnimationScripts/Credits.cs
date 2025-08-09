using UnityEngine;
using UnityEngine.Animations;

public class Credits : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 50f; // Pixels per second
    [SerializeField] private RectTransform creditsPanel;

    private Vector2 startPosition;

    private void Start()
    {
        AudioHandler.Instance.PlayMusic(AudioHandler.Instance.mainTheme);
        if (creditsPanel == null)
            creditsPanel = GetComponent<RectTransform>();

        startPosition = creditsPanel.anchoredPosition;
    }

    private void Update()
    {
        // Move up over time
        creditsPanel.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
    }

    public void ResetCredits()
    {
        creditsPanel.anchoredPosition = startPosition;
    }
}
