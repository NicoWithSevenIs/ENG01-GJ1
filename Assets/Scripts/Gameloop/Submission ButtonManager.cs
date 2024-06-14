using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubmissionButtonManager : MonoBehaviour
{
    [SerializeField] private GameObject submitButton;
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.Game_Loop.ON_ENTRY_CAMERA_PAN_END, () => { this.submitButton.SetActive(true);  });
        EventBroadcaster.Instance.AddObserver(EventNames.Game_Loop.ON_STAGE_END, () => { this.submitButton.SetActive(false);});
    }

    
}
