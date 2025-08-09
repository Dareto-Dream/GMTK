using System.Collections;
using UnityEngine;
using UnityEngine.UI; // if you need Image

[RequireComponent(typeof(CanvasGroup))]
public class KillSwitchAnimation : MonoBehaviour
{
    [Header("Animation settings")]
    private float duration = 1f;         // total animation time
    private float peakScale = 1.2f;        // scale at the "pop"
    public Vector3 baseScale = Vector3.one; // final scale
    public bool hideOnEnd = true;         // hide when finished (or Destroy)

    // runtime
    CanvasGroup cg;
    Coroutine running;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>();
        // start hidden
        cg.alpha = 0f;
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    // Call this from other scripts to play the effect
    public void Play()
    {
        // If already playing, restart cleanly
        if (running != null)
        {
            StopCoroutine(running);
            running = null;
        }

        gameObject.SetActive(true);
        running = StartCoroutine(AnimateCoroutine());
    }

    IEnumerator AnimateCoroutine()
    {
        float t = 0f;
        float half = duration * 0.35f; // time to reach peak
        float fadeOutStart = duration * 0.45f; // when alpha begins decreasing

        // start state
        transform.localScale = Vector3.zero;
        cg.alpha = 0f;

        while (t < duration)
        {
            t += Time.deltaTime;
            // Scale: ease out to peak then settle to baseScale
            if (t < half)
            {
                float p = t / half; // 0..1
                // ease out (sin)
                float s = Mathf.Sin(p * Mathf.PI * 0.5f);
                transform.localScale = Vector3.Lerp(Vector3.zero, baseScale * peakScale, s);
            }
            else
            {
                float p = (t - half) / (duration - half); // 0..1
                // ease back from peak to baseScale
                float s = 1f - Mathf.Pow(1f - p, 2f); // ease in
                transform.localScale = Vector3.Lerp(baseScale * peakScale, baseScale, s);
            }

            // Alpha: ramp up quickly, then fade out
            if (t < fadeOutStart)
            {
                // fade in (fast)
                float p = Mathf.Clamp01(t / fadeOutStart);
                cg.alpha = Mathf.SmoothStep(0f, 1f, p);
            }
            else
            {
                // fade out
                float p = Mathf.Clamp01((t - fadeOutStart) / (duration - fadeOutStart));
                cg.alpha = Mathf.SmoothStep(1f, 0f, p);
            }

            yield return null;
        }

        // ensure final state
        cg.alpha = 0f;
        transform.localScale = baseScale;

        running = null;

        if (hideOnEnd)
            gameObject.SetActive(false);
        // Or: Destroy(gameObject);
    }
}
