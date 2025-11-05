using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ProgressBarController : MonoBehaviour
{
    public Image fillImage;
    public RectTransform marker; // ✅ assign the moving image here
    public int totalTaps = 5;

    private int currentTaps = 0;
    private Coroutine currentAnim;
    public Action OnProgressFull;

    private RectTransform fillRect;
    private float fillStartX;
    private float fillEndX;

    void Start()
    {
        fillRect = fillImage.GetComponent<RectTransform>();

        // Record left and right positions of the fill area (in local space)
        fillStartX = fillRect.rect.xMin;
        fillEndX = fillRect.rect.xMax;

        // Reset marker position
        UpdateMarkerPosition(0f);
    }

    public void ResetBar()
    {
        currentTaps = 0;
        fillImage.fillAmount = 0f;
        UpdateMarkerPosition(0f);
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
            t += Time.deltaTime * 3f;
            float currentFill = Mathf.Lerp(start, target, t);
            fillImage.fillAmount = currentFill;

            // Move marker according to fill
            UpdateMarkerPosition(currentFill);

            yield return null;
        }

        fillImage.fillAmount = target;
        UpdateMarkerPosition(target);

        if (Mathf.Approximately(target, 1f) || fillImage.fillAmount >= 0.99f)
        {
            OnProgressFull?.Invoke();
        }
    }

    private void UpdateMarkerPosition(float fillAmount)
    {
        if (marker == null || fillRect == null) return;

        // Get width of the fill image area
        float width = fillRect.rect.width;
        float newX = Mathf.Lerp(fillStartX, fillEndX, fillAmount);
        Vector3 markerPos = marker.localPosition;
        markerPos.x = newX;
        marker.localPosition = markerPos;
        markerPos.y = Mathf.Sin(Time.time * 5f) * 5f; // gentle bobbing

    }

    public bool IsFull()
    {
        return fillImage.fillAmount >= 0.99f;
    }
}
