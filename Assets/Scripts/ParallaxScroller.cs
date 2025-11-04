using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    public float speed = 1f;           // How fast this layer moves
    public float resetPositionX = -20; // Where to reset
    public float startPositionX = 20;  // Where to reappear

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Move left continuously
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Loop position
        if (transform.position.x <= resetPositionX)
        {
            transform.position = new Vector3(startPositionX, startPos.y, startPos.z);
        }
    }
}
