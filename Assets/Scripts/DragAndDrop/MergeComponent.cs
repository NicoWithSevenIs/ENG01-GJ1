using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeComponent : MonoBehaviour
{
    [SerializeField] private GameObject currentObject;

    private void processMergeComponents(GameObject dragged, GameObject other)
    {
        Vector3 position = other.transform.position;
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

    private bool checkComponentDrag()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.gameObject != null)
                {
                    if (hit.collider.gameObject.GetComponent<ComponentDragDrop>() != null && hit.collider.gameObject.GetComponent<ComponentScript>() != null)
                    {
                        this.currentObject = hit.collider.gameObject;
                        return true;
                    }
                }
            }
        }

        this.currentObject = null;
        return false;
    }

    public void FireMergeEvent(GameObject currentObject, GameObject targetObject)
    {
        if (this.checkComponentDrag())
        {
            this.processMergeComponents(this.currentObject, targetObject);
        }

    }

    private void Awake()
    {
      
    }

}


