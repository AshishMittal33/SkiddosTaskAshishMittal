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

        // auto center calculation
        Vector3 centerPoint = (bird.startPosition + pig.startPosition) / 2f;
        bird.endPosition = centerPoint;
        pig.endPosition = centerPoint;

        progressBar.ResetBar();
        rewardPanel.SetActive(false);
        progressBar.OnProgressFull += HandleProgressFull;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameEnded)
        {
            bird.MoveOneStep();
            pig.MoveOneStep();
            progressBar.IncrementProgress();

            var birdAnim = bird.GetComponentInChildren<BirdAnimator>();
            if (birdAnim != null) birdAnim.TriggerHop();
        }
    }

    private void HandleProgressFull()
    {
        if (gameEnded) return;
        gameEnded = true;
        StartCoroutine(FinishSequence());
    }

    private IEnumerator FinishSequence()
    {
        // Pig defeated
        var pigAnim = pig.GetComponentInChildren<PigAnimator>();
        if (pigAnim != null) pigAnim.Defeated();

        // Bird jumps onto Pig
        var birdAnim = bird.GetComponentInChildren<BirdAnimator>();
        if (birdAnim != null)
            birdAnim.JumpOntoPig(pig.transform);

        // Camera zoom
        Camera mainCam = Camera.main;
        float originalSize = mainCam.orthographicSize;
        float targetSize = originalSize * 0.8f;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 1.5f;
            mainCam.orthographicSize = Mathf.Lerp(originalSize, targetSize, Mathf.SmoothStep(0, 1, t));
            yield return null;
        }

        yield return new WaitForSeconds(3f);
        rewardPanel.SetActive(true);
    }

    void OnDestroy()
    {
        progressBar.OnProgressFull -= HandleProgressFull;
    }
}
