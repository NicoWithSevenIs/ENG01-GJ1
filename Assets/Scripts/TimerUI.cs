using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        EventBroadcaster.Instance.AddObserver(EventNames.Timer.ON_TIMER_TICK, UpdateValue);
    }

    private void UpdateValue(Parameters p)
    {
        image.fillAmount = 1 - p.GetFloatExtra("PROGRESS", 0.5f);
    }
}
