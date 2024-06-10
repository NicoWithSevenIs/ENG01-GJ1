using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropEvents : MonoBehaviour
{
   public static DragDropEvents instance;
   [SerializeField] private GameObject currentObject;

    public void FireMergeEvent(GameObject currentObject, GameObject targetObject)
    {
        if (this.checkComponentDrag())
        {
            //Debug.Log("Verified that an object is being dragged");
            MergeComponent.instance.processMerge(currentObject, targetObject);
        }
       
    }

    public void FireDivideEvent()
    {
        if (this.checkComponntRightClick())
        {
            Debug.Log("Fired divide event.");
        }
            
    }

    private bool checkComponentDrag ()
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

    private bool checkComponntRightClick()
    {
        if (Input.GetMouseButton(1))
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
                        //Debug.Log(currentObject.name + " is being dragged!");
                        return true;
                    }
                }
            }
        }

        this.currentObject = null;
        return false;
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

    private void Update()
    {
        this.FireDivideEvent();
    }

}


