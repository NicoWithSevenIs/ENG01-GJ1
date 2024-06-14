using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCameraPan : MonoBehaviour
{

    [SerializeField] private float rotateSpeed = 20.0f;

    private bool isRotating = false;
    private bool isRotationReversed = false;

    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.Game_Loop.ON_CAMERA_PAN_START, TriggerCamera);
    }

    public void TriggerCamera(Parameters p)
    {
        isRotating = true;
        isRotationReversed = p.GetBoolExtra("IS_REVERSED", false);
    }

    // Update is called once per frame
    void Update()
    {

        if (!isRotating)
            return;

        Vector3 pos = this.transform.eulerAngles;

        //gonna be hard coding this to save time
        if (!isRotationReversed) 
            if(pos.x >= 300f || pos.x <= 15f) {
                pos.x -= Time.deltaTime * rotateSpeed;
            }
            else
            {
                EventBroadcaster.Instance.PostEvent(EventNames.Game_Loop.ON_CAMERA_PAN_END);
                isRotating = false;
            }
        else
        {
            /*
            if (pos.x >= 300f || pos.x <= 15f)
            {
                pos.x -= Time.deltaTime * rotateSpeed;
            }
            else
            {
                EventBroadcaster.Instance.PostEvent(EventNames.Game_Loop.ON_CAMERA_PAN_END);
                isRotating = false;
            }
            */
        }

  

        transform.eulerAngles = pos;
        
        
    }
}
