using UnityEngine;
using System.Collections;

public class PigAnimator : MonoBehaviour
{
    public AudioSource hopSFX;
    public AudioSource defeatSFX;
    public SpriteRenderer spriteRenderer;

    [Header("Hop Settings")]
    public float hopHeight = 0.3f;
    public float hopSpeed = 3f;
    public float scaleBounce = 0.1f;

    private bool defeated = false;
    private Vector3 baseLocalPos;
    private Vector3 baseScale;
    private Color baseColor;

    void Start()
    {
        baseLocalPos = transform.localPosition;
        baseScale = transform.localScale;
        baseColor = spriteRenderer.color;
        StartCoroutine(HopLoop());
    }

    private IEnumerator HopLoop()
    {
        while (!defeated)
        {
            yield return HopOnce();
            yield return new WaitForSeconds(0.3f);
        }
    }

    private IEnumerator HopOnce()
    {
        if (hopSFX) hopSFX.Play();
        float t = 0f;
        Vector3 start = baseLocalPos;
        Vector3 peak = start + Vector3.up * hopHeight;

        while (t < 1f)
        {
            t += Time.deltaTime * hopSpeed;
            float smooth = Mathf.Sin(t * Mathf.PI);
            transform.localPosition = Vector3.Lerp(start, peak, smooth);

            float scaleY = 1f - Mathf.Sin(t * Mathf.PI) * scaleBounce;
            transform.localScale = new Vector3(baseScale.x, scaleY, baseScale.z);
            yield return null;
        }

        transform.localPosition = baseLocalPos;
        transform.localScale = baseScale;
    }

    public void Defeated()
    {
        defeated = true;
        StartCoroutine(DefeatAnim());
    }

    private IEnumerator DefeatAnim()
    {
        if (defeatSFX) defeatSFX.Play();
        float t = 0f;
        Color defeatColor = Color.gray;

        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            spriteRenderer.color = Color.Lerp(baseColor, defeatColor, t);
            float squish = Mathf.Lerp(1f, 0.6f, t);
            transform.localScale = new Vector3(baseScale.x * 1.2f, baseScale.y * squish, baseScale.z);
            yield return null;
        }
    }
}
