using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ProgressBarController : MonoBehaviour
{
    public Image fillImage;
    public int totalTaps = 5;
    private int currentTaps = 0;
    private Coroutine currentAnim;

    // 👉 Event callback when full
    public Action OnProgressFull;

    public void ResetBar()
    {
        currentTaps = 0;
        fillImage.fillAmount = 0f;
    }

    public void IncrementProgress()
    {
        if (currentTaps < totalTaps)
        {
            currentTaps++;
            float targetFill = (float)currentTaps / totalTaps;

            if (currentAnim != null)
                StopCoroutine(currentAnim);

            currentAnim = StartCoroutine(AnimateFill(targetFill));
        }
    }

    private IEnumerator AnimateFill(float target)
    {
        float start = fillImage.fillAmount;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 3f; // speed
            fillImage.fillAmount = Mathf.Lerp(start, target, t);
            yield return null;
        }

        fillImage.fillAmount = target;

        // ✅ Trigger event if full
        if (Mathf.Approximately(target, 1f) || fillImage.fillAmount >= 0.99f)
        {
            OnProgressFull?.Invoke();
        }
    }

    public bool IsFull()
    {
        return fillImage.fillAmount >= 0.99f;
    }
}
