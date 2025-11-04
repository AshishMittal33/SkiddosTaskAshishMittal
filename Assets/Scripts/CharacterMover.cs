using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public Vector3 startPosition;
    public Vector3 endPosition;
    public int totalTapsToCenter = 5; // configurable
    private int currentTap = 0;

    public void ResetPosition()
    {
        transform.position = startPosition;
        currentTap = 0;
    }

    public void MoveOneStep()
    {
        if (currentTap < totalTapsToCenter)
        {
            currentTap++;
            float t = (float)currentTap / totalTapsToCenter;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
        }
    }
}
