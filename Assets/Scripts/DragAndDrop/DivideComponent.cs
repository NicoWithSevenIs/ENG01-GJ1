using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivideComponent : MonoBehaviour
{
    public static DivideComponent instance;

    public void processDivide(GameObject currentObject)
    {
        RecipeComponentPool.instance.processDivideComponents(currentObject, currentObject.transform.position);
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
