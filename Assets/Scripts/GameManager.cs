using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public CharacterMover bird;
    public CharacterMover pig;
    public ProgressBarController progressBar;
    public GameObject rewardPanel;

    public int tapsToMeet = 5;

    private bool gameEnded = false;

    void Start()
    {
        bird.totalTapsToCenter = tapsToMeet;
        pig.totalTapsToCenter = tapsToMeet;
        progressBar.totalTaps = tapsToMeet;

        progressBar.ResetBar();
        rewardPanel.SetActive(false);

        // ✅ Listen to event
        progressBar.OnProgressFull += HandleProgressFull;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameEnded)
        {
            bird.MoveOneStep();
            pig.MoveOneStep();
            progressBar.IncrementProgress();
        }
    }

    private void HandleProgressFull()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            StartCoroutine(FinishSequence());
        }
    }

    private IEnumerator FinishSequence()
    {
        // Bird hops onto pig’s head
        Vector3 target = pig.transform.position + new Vector3(0, 1.5f, 0);
        Vector3 start = bird.transform.position;
        float t = 0;

        while (t < 1f)
        {
            t += Time.deltaTime * 2;
            bird.transform.position = Vector3.Lerp(start, target, Mathf.SmoothStep(0, 1, t));
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        // Show reward panel
        rewardPanel.SetActive(true);
    }

    void OnDestroy()
    {
        // Clean up subscription
        progressBar.OnProgressFull -= HandleProgressFull;
    }
}
