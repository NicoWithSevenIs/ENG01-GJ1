using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropEvents : MonoBehaviour
{
   public static DragDropEvents instance;

    public void FireMergeEvent(GameObject currentObject, GameObject targetObject)
    {

        if (currentObject.GetComponent<ComponentScript>() != null && targetObject.GetComponent<ComponentScript>() != null)
        {
            ComponentScript currentComponent = currentObject.GetComponent<ComponentScript>();
            ComponentScript targetComponent = targetObject.GetComponent<ComponentScript>();
            MergeComponent.instance.processMerge(currentComponent, targetComponent);
        }

    }


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
}

