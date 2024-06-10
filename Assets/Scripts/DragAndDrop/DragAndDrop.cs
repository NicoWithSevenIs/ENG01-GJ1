using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Vector3 mousePosition;

    protected Vector3 GetMousePos()
    {
        return Camera.main.WorldToScreenPoint(this.transform.position);
    }

    protected void OnMouseDown()
    {
        this.mousePosition = Input.mousePosition - GetMousePos();
     }

    protected void OnMouseDrag()
    {
        this.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition - mousePosition);
    }
    
}

