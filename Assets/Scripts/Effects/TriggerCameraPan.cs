using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCameraPan : MonoBehaviour
{

    [SerializeField] private Transform target;
    [SerializeField] private TransformBottle bottleScript;

    private float rotateSpeed = 20.0f;
    private float rotateThreshold = 3.3f;
    private bool willLookAtTable = true;
    
  
    // Update is called once per frame
    void Update()
    {
      
        Vector3 pos = this.transform.eulerAngles;

        print(pos.x);
        if (this.bottleScript.triggerCam && (pos.x >= 300f || pos.x <= 15f)) {


            pos.x -= Time.deltaTime * rotateSpeed;

        }
        else if (!this.bottleScript.triggerCam && pos.x <= 0.0f)
        {
            pos.x += Time.deltaTime * rotateSpeed;
                
        }

        transform.eulerAngles = pos;
        
        
    }
}
