using UnityEngine;

public class BounceEffect : MonoBehaviour
{
    public float amplitude = 0.2f;
    public float frequency = 3f;
    private float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        float newY = startY + Mathf.Sin(Time.time * frequency) * amplitude;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
