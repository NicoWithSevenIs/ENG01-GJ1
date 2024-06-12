using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpurityArea : MonoBehaviour
{
    [SerializeField] private Transform dropArea;
    [SerializeField] private Transform impurityArea;


    private void OnTriggerStay(Collider other)
    {
        ComponentData data = other.gameObject.GetComponent<ComponentScript>()?.Data;

        if (data == null)
            return;

        other.transform.parent = impurityArea;
     

    }

    private void OnTriggerExit(Collider other)
    {
        ComponentData data = other.gameObject.GetComponent<ComponentScript>()?.Data;

        if (data == null)
            return;


        other.transform.parent = dropArea;
    }


}
