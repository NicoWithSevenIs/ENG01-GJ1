using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameTimer : Timer
{

    protected override void Start()
    {
        base.Start();
        this.StartTimer();
    }
    protected override void onElapse()
    {
        Parameters p = new Parameters();
        p.PutExtra("IS_FORCED", true);
        EventBroadcaster.Instance.PostEvent(EventNames.Game_Loop.ON_POTION_SUBMISSION, p);
    }

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.Game_Loop.ON_STAGE_START, this.StartTimer);
    }

    protected override void onTick()
    {
        Parameters p = new Parameters();
        p.PutExtra("PROGRESS", this.getPercentageCompletion());

        EventBroadcaster.Instance.PostEvent(EventNames.Timer.ON_TIMER_TICK, p);
    }

}
