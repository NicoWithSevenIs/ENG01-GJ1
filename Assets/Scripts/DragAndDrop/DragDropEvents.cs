using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropEvents : MonoBehaviour
{
   public static DragDropEvents instance;

   public bool checkComponentCollision(GameObject currentObject, GameObject targetObject)
   {
        if (currentObject.GetComponent<DragAndDrop>() != null && targetObject.GetComponent<DragAndDrop>() != null)
        {
            Collider collider1 = currentObject.GetComponent<Collider>();
            Collider collider2 = targetObject.GetComponent<Collider>();
            if (collider1.bounds.Intersects(collider2.bounds))
            {
                currentObject.GetComponent<DragAndDrop>().IsColliding = true;
                return true;

            }
            else
            {
                currentObject.GetComponent<DragAndDrop>().IsColliding = false;
                return false;
            }
        }

        return false;
   }

    public void callMerge(GameObject currentObject, GameObject targetObject)
    {
        MergeComponent.instance.invokeComponentMerge(currentObject, targetObject);
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

