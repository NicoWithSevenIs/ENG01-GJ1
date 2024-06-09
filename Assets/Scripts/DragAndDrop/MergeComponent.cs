using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeComponent : MonoBehaviour
{
    public static MergeComponent instance;
    
    public void processMerge(GameObject currentObject, GameObject targetObject)
    {
        this.processMergeLevel1(currentObject, targetObject);
        this.processMergeLevel2(currentObject, targetObject);
    }

    private void processMergeLevel1(GameObject currentObject, GameObject targetObject)
    {
        ComponentScript currentComponent = currentObject.GetComponent<ComponentScript>();
        ComponentScript targetComponent = targetObject.GetComponent<ComponentScript>();

        if (currentComponent.Data.ComponentName == "Cyan" && targetComponent.Data.ComponentName == "Gray")
        {
           this.invokeMerge(currentObject, targetObject, "Aegean");
        }

        else if (currentComponent.Data.ComponentName == "Magenta" && targetComponent.Data.ComponentName == "Gray")
        {
            this.invokeMerge(currentObject, targetObject, "Boysenberry");
        }

        else if (currentComponent.Data.ComponentName == "Yellow" && targetComponent.Data.ComponentName == "Gray")
        {
            this.invokeMerge(currentObject, targetObject, "Butterscotch");
        }

        else if (currentComponent.Data.ComponentName == "Cyan" && targetComponent.Data.ComponentName == "Yellow")
        {
            this.invokeMerge(currentObject, targetObject, "Lime");
        }

        else if (currentComponent.Data.ComponentName == "Magenta" && targetComponent.Data.ComponentName == "Yellow")
        {
            this.invokeMerge(currentObject, targetObject, "Scarlet");
        }

        else if (currentComponent.Data.ComponentName == "Cyan" && targetComponent.Data.ComponentName == "Magenta")
        {
            this.invokeMerge(currentObject, targetObject, "Lavender");
        }
       
    }

    private void processMergeLevel2(GameObject currentObject, GameObject targetObject)
    {
        ComponentScript currentComponent = currentObject.GetComponent<ComponentScript>();
        ComponentScript targetComponent = targetObject.GetComponent<ComponentScript>();

        if (currentComponent.Data.ComponentName == "Lavender" && targetComponent.Data.ComponentName == "Cyan")
        {
            this.invokeMerge(currentObject, targetObject, "Arctic");
        }

        else if (currentComponent.Data.ComponentName == "Yellow" && targetComponent.Data.ComponentName == "Butterscotch")
        {
            this.invokeMerge(currentObject, targetObject, "Gold");
        }

        else if (currentComponent.Data.ComponentName == "Gray" && targetComponent.Data.ComponentName == "Lime")
        {
            this.invokeMerge(currentObject, targetObject, "Dark Green");
        }

        else if (currentComponent.Data.ComponentName == "Magenta" && targetComponent.Data.ComponentName == "Lime")
        {
            this.invokeMerge(currentObject, targetObject, "Bubblegum Pink");
        }

        else if (currentComponent.Data.ComponentName == "Boysenberry" && targetComponent.Data.ComponentName == "Lavender")
        {
            this.invokeMerge(currentObject, targetObject, "Violet");
        }

        else if (currentComponent.Data.ComponentName == "Scarlet" && targetComponent.Data.ComponentName == "Butterscotch")
        {
            this.invokeMerge(currentObject, targetObject, "Spice");
        }

        else if (currentComponent.Data.ComponentName == "Scarlet" && targetComponent.Data.ComponentName == "Aegean")
        {
            this.invokeMerge(currentObject, targetObject, "Charcoal");
        }

        else if (currentComponent.Data.ComponentName == "Gray" && targetComponent.Data.ComponentName == "Gray")
        {
            this.invokeMerge(currentObject, targetObject, "Scotch Mist");
        }

    }

    private void invokeMerge(GameObject currentObject, GameObject targetObject, string name)
    {
        Vector3 currentObjectPos = currentObject.transform.position;
        currentObject.SetActive(false);
        targetObject.SetActive(false);
        RecipeComponentPool.instance.clone(name, currentObjectPos);
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

    private void Start()
    {

    }

    private void Update()
    {

    }
}


