using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCameraPan : MonoBehaviour
{

    [SerializeField] private float rotateSpeed = 20.0f;
    [SerializeField] private float zoomDistance =  3f;
    [SerializeField] private float zoomSpeed = 20f;

    private bool isRotating = false;
    private bool isRotationReversed = false;

    private bool isZooming = false;
    private bool isZoomingOut = false;

    private float yDistanceBeforeZoom = 0f;


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

        handleZoom();


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
                yDistanceBeforeZoom = transform.position.y;
                isZooming = true;
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

    private void handleZoom()
    {
        if(!isZooming) 
            return;

        if (!isZoomingOut)
        {
            if(Mathf.Abs(transform.position.y - yDistanceBeforeZoom) < zoomDistance)
            {
                Vector3 pos = transform.position;
                pos.y -= Time.deltaTime * zoomSpeed;
                transform.position = pos;
            }
            else
            {
                EventBroadcaster.Instance.PostEvent(EventNames.Game_Loop.ON_CAMERA_PAN_END);
                isZooming= false;
            }
        }

        
    }
}
