using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeComponent : MonoBehaviour
{
    public static MergeComponent instance;
    
    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void processMerge(ComponentScript currentComponent, ComponentScript targetComponent)
    {
        if (currentComponent.Data.ComponentName == "Cyan" && targetComponent.Data.ComponentName == "Gray")
        {
            Debug.Log("Aegean color made!");
        }
    }

    private void processMergeLevel1(ComponentScript currentComponent, ComponentScript targetComponent)
    {

    }
}


