using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivideComponent : MonoBehaviour
{
    public static DivideComponent instance;

    public void processDivide(GameObject currentObject)
    {
       processDivideComponents(currentObject, currentObject.transform.position);
    }

    public void processDivideComponents(GameObject currentObject, Vector3 position)
    {
        string currentComponentName = currentObject.GetComponent<ComponentScript>().Data.ComponentName;

        ComponentData data = currentObject.GetComponent<ComponentScript>().Data;

        //If the player tries to divide a base component
        if(data.ComponentA == null || data.ComponentB == null)
            return;
        
        string componentAName = data.ComponentA.ComponentName;
        string componentBName = data.ComponentB.ComponentName;
    
       
        ComponentDirector.Instance.getPoolableInstance(componentAName, position + Vector3.right);
        ComponentDirector.Instance.getPoolableInstance(componentBName, position - Vector3.right);
        ComponentDirector.Instance.setPoolableInactive(currentObject);

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
