using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeComponent : MonoBehaviour
{
    public static MergeComponent Instance;
    
    public void processMerge(GameObject currentObject, GameObject targetObject)
    {
        processMergeComponents(currentObject, targetObject, targetObject.transform.position);
    }

    public void processMergeComponents(GameObject dragged, GameObject other, Vector3 position)
    {

        ComponentBlueprint blueprint = ComponentDirector.Instance.getBlueprint();

        if (!ComponentDirector.Instance.isObjectInPool(dragged) || !ComponentDirector.Instance.isObjectInPool(other))
            return;

        ComponentData draggedData = dragged.GetComponent<ComponentScript>()?.Data;
        ComponentData otherData = other.GetComponent<ComponentScript>()?.Data;

        if (draggedData == null || otherData == null)
            return;

        foreach (var tier in blueprint.Blueprints)
        {
            foreach (var componentData in tier.DataList)
            {
                if (componentData.ComponentA == null || componentData.ComponentB == null)
                    continue;

                string a = componentData.ComponentA.ComponentName;
                string b = componentData.ComponentB.ComponentName;

                if (a == draggedData.ComponentName && b == otherData.ComponentName || a == otherData.ComponentName && b == draggedData.ComponentName)
                {
                    ComponentDirector.Instance.getPoolableInstance(componentData.ComponentName, dragged.transform.position);
                    ComponentDirector.Instance.setPoolableInactive(dragged);
                    ComponentDirector.Instance.setPoolableInactive(other);
                   
                    break;
                }

            }
        }

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

}


