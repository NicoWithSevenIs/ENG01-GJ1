using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentDragDrop : DragAndDrop
{
    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.GetComponent<ComponentDragDrop>() != null && collision.gameObject.GetComponent<ComponentScript>() != null)
        {
            //this.targetObject = collision.gameObject;
            DragDropEvents.instance.FireMergeEvent(this.gameObject, collision.gameObject);
        }
    }

}
