using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Vector3 mousePosition;

    [SerializeField] private string itemName;

    public string ItemName { get { return itemName; } set { itemName = value; } }


    [SerializeField] private bool isColliding;

    public bool IsColliding { get { return isColliding; } set { isColliding = value; } }

    [SerializeField] private bool isDividing;

    public bool IsDividing { get { return isDividing; } set { isDividing = value; } }

    [SerializeField] private bool isComponent;

    private Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(this.transform.position);
    }

    private void OnMouseDown()
    {
        this.mousePosition = Input.mousePosition - GetMousePos();
    }

    private void OnMouseDrag()
    {
        this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }
    
    private void Start()
    {
        this.isColliding = false;
        this.isComponent = false;
        this.isDividing = false;
        
    }

    private void OnCollisionEnter(Collision collider)
    {
        if (DragDropEvents.instance.checkComponentCollision(this.gameObject, collider.gameObject))
        {
            DragDropEvents.instance.callMerge(this.gameObject, collider.gameObject);
        }
    }

}

