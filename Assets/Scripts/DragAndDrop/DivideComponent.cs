using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivideComponent : MonoBehaviour
{
    public static DivideComponent instance;

    public void processsDivide(GameObject currentObject, GameObject targetObject)
    {
       
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
