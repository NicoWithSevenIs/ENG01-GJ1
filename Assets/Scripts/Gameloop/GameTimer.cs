using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameTimer : Timer
{
    protected override void onElapse()
    {
        Parameters p = new Parameters();
        p.PutExtra("IS_FORCED", true);
        EventBroadcaster.Instance.PostEvent(EventNames.Game_Loop.ON_POTION_SUBMISSION, p);
    }
}
