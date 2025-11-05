using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonContinuousPop : MonoBehaviour
{
    [Header("Pop Animation Settings")]
    public float minScale = 0.9f;       // Smallest scale
    public float maxScale = 1.1f;       // Largest scale
    public float speed = 2f;            // Speed of pulsing
    public bool autoStart = true;       // Start animation automatically
    public bool playOnceOnEnable = false; // optional single pop instead of loop

    private Vector3 originalScale;
    private Coroutine pulseRoutine;

    void OnEnable()
    {
        originalScale = transform.localScale;

        if (pulseRoutine != null)
            StopCoroutine(pulseRoutine);

        if (autoStart)
        {
            if (playOnceOnEnable)
                pulseRoutine = StartCoroutine(SinglePop());
            else
                pulseRoutine = StartCoroutine(PulseLoop());
        }
    }

    private IEnumerator PulseLoop()
    {
        while (true)
        {
            // Grow to max
            yield return ScaleTo(originalScale * maxScale);
            // Shrink to min
            yield return ScaleTo(originalScale * minScale);
        }
    }

    private IEnumerator SinglePop()
    {
        // For one pop (like scene start)
        yield return ScaleTo(originalScale * maxScale);
        yield return ScaleTo(originalScale);
    }

    private IEnumerator ScaleTo(Vector3 target)
    {
        while (Vector3.Distance(transform.localScale, target) > 0.001f)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, target, Time.deltaTime * speed);
            yield return null;
        }
        transform.localScale = target;
    }

    // Optional scene change on button click
    public void SceneChange(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
