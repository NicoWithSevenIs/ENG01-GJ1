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

    private float initialXRotation;


    private void Awake()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.Game_Loop.ON_ENTRY_CAMERA_PAN_START, LookAtTable);
        EventBroadcaster.Instance.AddObserver(EventNames.Game_Loop.ON_EXIT_CAMERA_PAN_START, LookAwayFromTable);
        this.initialXRotation = transform.eulerAngles.x;
        print(this.initialXRotation);
    }

    public void LookAtTable()
    {
        isRotating = true;
        isRotationReversed = false;
    }
    public void LookAwayFromTable()
    {
       // print("Invoked");
        isZooming = true;
        isZoomingOut = true;
     
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
                isZoomingOut = false;
                isRotating = false;
            }
        else
        {
            
            if ((int)pos.x != initialXRotation)
            {
                pos.x += Time.deltaTime * rotateSpeed;
            }
            else
            {

                isRotating = false;

                 EventBroadcaster.Instance.PostEvent(EventNames.Game_Loop.ON_EXIT_CAMERA_PAN_END);
        
            }
           
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
                EventBroadcaster.Instance.PostEvent(EventNames.Game_Loop.ON_ENTRY_CAMERA_PAN_END);
                isZooming= false;
            }
        }
        else
        {
            if (yDistanceBeforeZoom - transform.position.y >= 0)
            {
                Vector3 pos = transform.position;
                pos.y += Time.deltaTime * zoomSpeed;
                transform.position = pos;
            }
            else
            {
                isRotating = true;
                isRotationReversed = true;
                print("ok");
                isZooming = false;
            }
        }

        
    }
}
