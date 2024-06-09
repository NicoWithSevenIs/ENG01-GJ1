using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Vector3 mousePosition;

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
    
}

