using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentDragDrop : DragAndDrop
{

    //[SerializeField] private GameObject targetObject;

    //public GameObject TargetObject
    //{
    //    get { return targetObject; }
    //    set { targetObject = value; }
    //}
    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.GetComponent<ComponentDragDrop>() != null && collision.gameObject.GetComponent<ComponentScript>() != null)
        {
            //this.targetObject = collision.gameObject;
            DragDropEvents.instance.FireMergeEvent(this.gameObject, collision.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
