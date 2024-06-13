using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentDragDrop : DragAndDrop
{
    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.GetComponent<ComponentDragDrop>() != null && collision.gameObject.GetComponent<ComponentScript>() != null)
        {
            Parameters param = new Parameters();
            param.PutExtra("TARGET_OBJECT", collision.gameObject);
            EventBroadcaster.Instance.PostEvent(EventNames.PotionComponents.ON_COMPONENT_MERGE, param);

        }
    }

}
