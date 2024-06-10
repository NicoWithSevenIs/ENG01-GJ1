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
        Vector3 currentObjectPos = currentObject.transform.position;
        RecipeComponentPool.instance.processMergeComponents(currentObject, targetObject, "Aegean", currentObjectPos);
        RecipeComponentPool.instance.processMergeComponents(currentObject, targetObject, "Boysenberry", currentObjectPos);
        RecipeComponentPool.instance.processMergeComponents(currentObject, targetObject, "Butterscotch", currentObjectPos);
        RecipeComponentPool.instance.processMergeComponents(currentObject, targetObject, "Lime", currentObjectPos);
        RecipeComponentPool.instance.processMergeComponents(currentObject, targetObject, "Scarlet", currentObjectPos);
        RecipeComponentPool.instance.processMergeComponents(currentObject, targetObject, "Lavender", currentObjectPos);

    }

    private void processMergeLevel2(GameObject currentObject, GameObject targetObject)
    {
        Vector3 currentObjectPos = currentObject.transform.position;
        RecipeComponentPool.instance.processMergeComponents(currentObject, targetObject, "Arctic", currentObjectPos);
        RecipeComponentPool.instance.processMergeComponents(currentObject, targetObject, "Gold", currentObjectPos);
        RecipeComponentPool.instance.processMergeComponents(currentObject, targetObject, "Dark Green", currentObjectPos);
        RecipeComponentPool.instance.processMergeComponents(currentObject, targetObject, "Bubblegum Pink", currentObjectPos);
        RecipeComponentPool.instance.processMergeComponents(currentObject, targetObject, "Violet", currentObjectPos);
        RecipeComponentPool.instance.processMergeComponents(currentObject, targetObject, "Spice", currentObjectPos);
        RecipeComponentPool.instance.processMergeComponents(currentObject, targetObject, "Charcoal", currentObjectPos);
        RecipeComponentPool.instance.processMergeComponents(currentObject, targetObject, "Scotch Mist", currentObjectPos);

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


