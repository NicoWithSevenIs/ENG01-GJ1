using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeComponent : MonoBehaviour
{
    public static MergeComponent Instance;
    
    public void processMerge(GameObject currentObject, GameObject targetObject)
    {
        this.processMergeLevel1(currentObject, targetObject);
        this.processMergeLevel2(currentObject, targetObject);
    }

    private void processMergeLevel1(GameObject currentObject, GameObject targetObject)
    {   
        Vector3 currentObjectPos = currentObject.transform.position;
        RecipeComponentPool.Instance.processMergeComponents(currentObject, targetObject, "Aegean", currentObjectPos);
        RecipeComponentPool.Instance.processMergeComponents(currentObject, targetObject, "Boysenberry", currentObjectPos);
        RecipeComponentPool.Instance.processMergeComponents(currentObject, targetObject, "Butterscotch", currentObjectPos);
        RecipeComponentPool.Instance.processMergeComponents(currentObject, targetObject, "Lime", currentObjectPos);
        RecipeComponentPool.Instance.processMergeComponents(currentObject, targetObject, "Scarlet", currentObjectPos);
        RecipeComponentPool.Instance.processMergeComponents(currentObject, targetObject, "Lavender", currentObjectPos);

    }

    private void processMergeLevel2(GameObject currentObject, GameObject targetObject)
    {
        Vector3 currentObjectPos = currentObject.transform.position;
        RecipeComponentPool.Instance.processMergeComponents(currentObject, targetObject, "Arctic", currentObjectPos);
        RecipeComponentPool.Instance.processMergeComponents(currentObject, targetObject, "Gold", currentObjectPos);
        RecipeComponentPool.Instance.processMergeComponents(currentObject, targetObject, "Dark Green", currentObjectPos);
        RecipeComponentPool.Instance.processMergeComponents(currentObject, targetObject, "Bubblegum Pink", currentObjectPos);
        RecipeComponentPool.Instance.processMergeComponents(currentObject, targetObject, "Violet", currentObjectPos);
        RecipeComponentPool.Instance.processMergeComponents(currentObject, targetObject, "Spice", currentObjectPos);
        RecipeComponentPool.Instance.processMergeComponents(currentObject, targetObject, "Charcoal", currentObjectPos);
        RecipeComponentPool.Instance.processMergeComponents(currentObject, targetObject, "Scotch Mist", currentObjectPos);

    }
    
    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {

    }

    private void Update()
    {

    }
}


