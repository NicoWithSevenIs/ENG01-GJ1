using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentDragDrop : DragAndDrop
{

    private void OnCollisionEnter(Collision collision)
    {
        DragDropEvents.instance.FireMergeEvent(this.gameObject, collision.gameObject);
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
