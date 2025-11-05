using UnityEngine;
using System.Collections;

public class BirdAnimator : MonoBehaviour
{
    public AudioSource hopSFX;
    public AudioSource happySFX;

    [Header("Hop Settings")]
    public float hopHeight = 0.4f;
    public float hopSpeed = 6f;
    public float scaleBounce = 0.15f;

    private bool isOnPig = false;
    private bool canHop = true;
    private Vector3 baseLocalPos;
    private Vector3 baseScale;

    void Start()
    {
        baseLocalPos = transform.localPosition;
        baseScale = transform.localScale;
        StartCoroutine(IdleAnimation());
    }

    public void TriggerHop()
    {
        if (canHop && !isOnPig)
            StartCoroutine(HopAnimation());
    }

    private IEnumerator HopAnimation()
    {
        canHop = false;
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
        canHop = true;
    }

    private IEnumerator IdleAnimation()
    {
        while (!isOnPig)
        {
            float scale = 1f + Mathf.Sin(Time.time * 2f) * 0.03f;
            transform.localScale = new Vector3(baseScale.x * scale, baseScale.y * scale, baseScale.z);
            yield return null;
        }
    }

    public void JumpOntoPig(Transform pig)
    {
        StartCoroutine(JumpToPigRoutine(pig.position + new Vector3(0, 1.3f, 0)));
    }

    private IEnumerator JumpToPigRoutine(Vector3 target)
    {
        isOnPig = true;
        canHop = false;
        Vector3 start = transform.parent.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * 2f;
            float height = Mathf.Sin(t * Mathf.PI);
            transform.parent.position = Vector3.Lerp(start, target, t) + Vector3.up * height * 1f;
            yield return null;
        }

        transform.parent.position = target;
        transform.localScale = baseScale;

        if (happySFX)
        {
            happySFX.loop = true;
            happySFX.Play();
        }

        StartCoroutine(HappyIdle());
    }

    private IEnumerator HappyIdle()
    {
        while (true)
        {
            float scale = 1f + Mathf.Sin(Time.time * 3f) * 0.05f;
            transform.localScale = new Vector3(baseScale.x * scale, baseScale.y * scale, baseScale.z);
            yield return null;
        }
    }
}
