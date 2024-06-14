using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmitUI : MonoBehaviour
{
   

    public void InvokeSubmission()
    {

        Parameters p = new Parameters();
        p.PutExtra("IS_FORCED", false);
        EventBroadcaster.Instance.PostEvent(EventNames.Game_Loop.ON_POTION_SUBMISSION,p);
    }
}
