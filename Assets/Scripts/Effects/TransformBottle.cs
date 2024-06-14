using Palmmedia.ReportGenerator.Core.Reporting.Builders;
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


    

    private void Awake()
    {
        this.topThreshold = this.transform.position.y - 1.9f;
        this.isFloat = false;
        this.triggerCam = false;
        //this.glow.SetActive(false);
    }

    private void levitateObject()
    { 
        
       
        if (isFloat)
        {
            Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
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
                StartCoroutine(delayedAction
                    (1.5f, () =>
                        { 
                            EventBroadcaster.Instance.PostEvent(EventNames.Game_Loop.ON_STAGE_START);
                            StartCoroutine(delayedAction(0.5f, () => { this.triggerCam = true; }));
                        }
                    )
                );
                this.isFloat = false;
            }
        }

        //Debug.Log("top threshold: " + this.topThreshold);
        //Debug.Log("position y: " + this.transform.position.y);
        
    }


    private IEnumerator delayedAction(float duration, Action action)
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();
        
    }


    private void Start()
    {
        //EventBroadcaster.Instance.
        StartCoroutine(delayedAction(3.5f, () => { this.isFloat = true; }));

    }

    private void handleHalo()
    {
        this.glow.SetActive(true);
        Light bottleGlow = this.glow.GetComponent<Light>();
        bottleGlow.range += 0.47f * Time.deltaTime;
        
    }

    private void OnDisable()
    {
        //remove observer here
    }

    // Update is called once per frame
    void Update()
    {
            this.levitateObject();
    }
}
