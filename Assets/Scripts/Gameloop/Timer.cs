using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] protected float maxDuration = 10f;
    [SerializeField] protected float timeElapsed;
    protected bool isRunning;

    protected virtual void Start()
    {
        this.StopTimer();
     
    }

    protected void Update()
    {
        if (!this.isRunning)
            return;

        this.timeElapsed += Time.deltaTime;
        this.onTick();

        if (this.timeElapsed >= this.maxDuration)
        {
            this.StopTimer();
            this.onElapse();
        }

    }

    protected virtual void onTick() {
        Debug.Log("This is being invoked instead");
    }
    protected virtual void onElapse(){
        Debug.Log("This is being invoked instead");
    }

    public void StartTimer()
    {
        this.isRunning = true;
    }

    public void StopTimer()
    {
        this.isRunning = false;
        this.timeElapsed = 0;
    }

    public float getPercentageCompletion()
    {
        return this.timeElapsed/ this.maxDuration;
    }

}
