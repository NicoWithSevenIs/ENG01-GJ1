using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//copy pasting since we're running out of time hahaha

public class LevitateComponent : MonoBehaviour
{
    [SerializeField] private float topThreshold;

    private bool isFloat;

    private bool hasBeenPurfied = false;

    private Rigidbody rb;

    private void Awake()
    {
        this.topThreshold = this.transform.position.y + 1.9f;
        this.isFloat = false;
    }


    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        StartCoroutine(delayedAction(0.5f, () => { this.isFloat = true; }));
    }


    private void levitateObject()
    {

        print("Running");
        if (this.transform.position.y < this.topThreshold)
        {
            float velocityY = (20.5f * Time.deltaTime);
            rb.velocity = new Vector3(rb.velocity.x, velocityY, rb.velocity.z);
            //this.transform.position = new Vector3(this.transform.position.x, positionY, this.transform.position.z);

            rb.useGravity = false;

        }
        



    }



    private IEnumerator delayedAction(float duration, Action action)
    {
        yield return new WaitForSeconds(duration);
        action?.Invoke();

    }

  

    private void FixedUpdate()
    {
        this.levitateObject();
    }
}
