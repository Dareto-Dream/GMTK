using UnityEngine;

public class BarrierLogic : MonoBehaviour
{
    private BoxCollider2D barrier;

    private void Awake()
    {
        barrier = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnLoopChanged += OnLoopChanged;
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnLoopChanged -= OnLoopChanged;
    }

    private void OnLoopChanged(int loop)
    {
        barrier.enabled = (loop == 4);
    }
}
