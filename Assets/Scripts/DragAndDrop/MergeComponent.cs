using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeComponent : MonoBehaviour
{
    public static MergeComponent instance;

    public void invokeComponentMerge(GameObject currentObject, GameObject targetObject)
    {
        if (currentObject.GetComponent<DragAndDrop>() != null && targetObject.GetComponent<DragAndDrop>() != null)
        {
            this.processComponentMerge(currentObject, targetObject);
        }
    }

    private void processComponentMerge(GameObject currentObject, GameObject targetObject)
    {
        DragAndDrop currentComponent = currentObject.GetComponent<DragAndDrop>();
        DragAndDrop targetComponent = targetObject.GetComponent<DragAndDrop>();
        if (currentComponent.ItemName == ItemNames.TEST_CYAN || targetComponent.ItemName == ItemNames.TEST_GRAY)
        {
            currentComponent.GetComponent<MeshRenderer>().material.color = Color.cyan;
            currentComponent.ItemName = ItemNames.TEST_AEGEAN;
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


