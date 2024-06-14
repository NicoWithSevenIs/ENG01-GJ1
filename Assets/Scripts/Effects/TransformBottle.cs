using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformBottle : MonoBehaviour
{
    [SerializeField] private float topThreshold;
    [SerializeField] private GameObject glow;
    private bool isFloat;
    public bool triggerCam;

    private bool hasBeenPurfied = false;

    private Rigidbody rb;

    private void Awake()
    {
        this.topThreshold = this.transform.position.y - 1.9f;
        this.isFloat = false;
        this.triggerCam = false;
        //this.glow.SetActive(false);
    }

   
    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        StartCoroutine(delayedAction(2f, () => { this.isFloat = true; }));
    }
   

    private void levitateObject()
    {
        if (hasBeenPurfied)
            return;
       
        if (isFloat)
        {
            if (this.transform.position.y < this.topThreshold)
            {
                float velocityY = (20.5f * Time.deltaTime);
                rb.velocity = new Vector3(rb.velocity.x, velocityY, rb.velocity.z);
                //this.transform.position = new Vector3(this.transform.position.x, positionY, this.transform.position.z);

                rb.useGravity = false;
                this.handleHalo();
            }
            else if (this.transform.position.y >= this.topThreshold)
            {
            
                
                Action a = () => {
                    StartCoroutine(delayedAction(0.5f, () => {
                        EventBroadcaster.Instance.PostEvent(EventNames.Game_Loop.ON_ENTRY_CAMERA_PAN_START);
                        gameObject.SetActive(false);
                        EventBroadcaster.Instance.PostEvent(EventNames.Game_Loop.ON_STAGE_START);

                    }));
                };

                StartCoroutine(delayedAction(0.75f, a));
                this.isFloat = false;
            }
        }

   
        
    }



    private IEnumerator delayedAction(float duration, Action action)
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();
        
    }

    private void handleHalo()
    {
        this.glow.SetActive(true);
        Light bottleGlow = this.glow.GetComponent<Light>();
        bottleGlow.range += 0.47f * Time.deltaTime;
        
    }

    private void FixedUpdate()
    {
        this.levitateObject();
    }
}
