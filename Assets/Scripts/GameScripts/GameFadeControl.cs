using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        if (fadeImage == null)
            Debug.LogError("Assign the fade Image in the inspector!");
    }

    /// <summary>
    /// Fade from transparent to black.
    /// </summary>
    public IEnumerator FadeOut()
    {
        yield return Fade(0f, 1f);
    }

    /// <summary>
    /// Fade from black back to transparent.
    /// </summary>
    public IEnumerator FadeIn()
    {
        yield return Fade(1f, 0f);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color c = fadeImage.color;
        // ensure starting alpha
        c.a = startAlpha;
        fadeImage.color = c;
        // animate
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime; // use unscaled if you want to ignore Time.timeScale
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            c.a = Mathf.Lerp(startAlpha, endAlpha, t);
            fadeImage.color = c;
            yield return null;
        }
        // ensure final alpha
        c.a = endAlpha;
        fadeImage.color = c;
    }
}
