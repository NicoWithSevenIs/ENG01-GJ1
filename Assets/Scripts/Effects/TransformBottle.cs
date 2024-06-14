using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformBottle : MonoBehaviour
{
    [SerializeField] private float topThreshold;
    [SerializeField] private GameObject glow;
    private bool isFloat;
    
    private void Awake()
    {
        this.topThreshold = this.transform.position.y - 1.7f;
        this.isFloat = false;
        //this.glow.SetActive(false);
    }

    private void levitateObject()
    { 
        if (Input.GetMouseButtonDown(0)) {
             this.isFloat = true;
        }
       
        if (isFloat)
        {
            Rigidbody rb = this.gameObject.GetComponent<Rigidbody>();
            if (this.transform.position.y < this.topThreshold)
            {
                float positionY = this.transform.position.y + (0.5f * Time.deltaTime);
                this.transform.position = new Vector3(this.transform.position.x, positionY, this.transform.position.z);
                rb.useGravity = false;
                this.handleHalo();
            }
            else if (this.transform.position.y >= this.topThreshold)
            {
                
                this.isFloat = false;
                
            }
        }

        //Debug.Log("top threshold: " + this.topThreshold);
        //Debug.Log("position y: " + this.transform.position.y);
        
    }

    private void handleHalo()
    {
        this.glow.SetActive(true);
        Light bottleGlow = this.glow.GetComponent<Light>();
        bottleGlow.range += 2.5f * Time.deltaTime;
        
    }
    // Update is called once per frame
    void Update()
    {
            this.levitateObject();
    }
}
