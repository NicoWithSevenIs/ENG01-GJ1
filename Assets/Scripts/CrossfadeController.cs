using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossfadeController : MonoBehaviour
{
    [SerializeField]
    Animator crossfadeAnimator;

    [SerializeField]
    float crossfadeDuration = 1.0f;

    [SerializeField]
    float plainDuration = 3.0f;

    [SerializeField]
    float nextDayDuration = 4.0f;

    [SerializeField]
    GameObject gameOverImage;

    [SerializeField]
    GameObject nextDayImage;
    private float delayTimer = 0.0f;

    [ContextMenu("ShowGameOver()")]
    public void ShowGameOver()
    {
        gameOverImage.SetActive(true);
        nextDayImage.SetActive(false);

        StartCoroutine(triggerCrossfade("ShowGameOver"));
    }

    [ContextMenu("HideGameOver()")]
    public void HideGameOver()
    {
        crossfadeAnimator.SetBool("GameOver", false);
    }

    [ContextMenu("ShowNextDay()")]
    public void ShowNextDay()
    {
        gameOverImage.SetActive(false);
        nextDayImage.SetActive(true);

        StartCoroutine(triggerCrossfade("NextDay"));
    }

    [ContextMenu("PlainCrossfade()")]
    public void PlainCrossfade()
    {
        gameOverImage.SetActive(false);
        nextDayImage.SetActive(false);

        StartCoroutine(triggerCrossfade("Plain"));
    }

    private void Update()
    {
        if (delayTimer > 0.0f)
        {
            delayTimer -= Time.deltaTime;
            crossfadeAnimator.SetFloat("DelayTimer", delayTimer);
        }
    }

    IEnumerator triggerCrossfade(string type)
    {
        crossfadeAnimator.SetTrigger("Crossfade");
        switch (type)
        {
            case "Plain":
                crossfadeAnimator.SetBool("GameOver", false);
                delayTimer = plainDuration;
                break;
            case "NextDay":
                crossfadeAnimator.SetBool("GameOver", false);
                delayTimer = nextDayDuration;
                break;
            case "ShowGameOver":
                crossfadeAnimator.SetBool("GameOver", true);
                break;
            // case "HideGameOver":
            //     crossfadeAnimator.SetBool("GameOver", false);
            //     break;
        }
        yield return new WaitForSeconds(crossfadeDuration);
    }

}
