using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivideComponent : MonoBehaviour
{
    [SerializeField] private GameObject currentObject;

    public void processDivideComponents()
    {
        Vector3 position = currentObject.transform.position;


        ComponentData data = currentObject.GetComponent<ComponentScript>().Data;

        //If the player tries to divide a base component
        if (data == null || data.ComponentA == null || data.ComponentB == null)
            return;

        string currentComponentName = currentObject.GetComponent<ComponentScript>().Data.ComponentName;
        string componentAName = data.ComponentA.ComponentName;
        string componentBName = data.ComponentB.ComponentName;
    
       
        ComponentDirector.Instance.getPoolableInstance(componentAName, position + Vector3.right);
        ComponentDirector.Instance.getPoolableInstance(componentBName, position - Vector3.right);
        ComponentDirector.Instance.setPoolableInactive(currentObject);

    }

    private bool checkComponentRightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider != null && hit.collider.gameObject != null)
                {
                    if (hit.collider.gameObject.GetComponent<ComponentDragDrop>() != null && hit.collider.gameObject.GetComponent<ComponentScript>() != null)
                    {
                        currentObject = hit.collider.gameObject;
                        return true;
                    }
                }
            }
        }

        currentObject = null;
        return false;
    }

    private void Update()
    {
        if (this.checkComponentRightClick())
        {
            processDivideComponents();
        }
    }
}
