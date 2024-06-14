using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] protected float maxDuration = 10f;
    protected float timeElapsed;
    protected bool isRunning;

    protected void Start()
    {
        this.StopTimer();
     
    }

    protected void Update()
    {
        if (!this.isRunning)
            return;

        this.timeElapsed += Time.deltaTime;

        if (this.timeElapsed > this.maxDuration)
        {
            this.StopTimer();
            this.onElapse();
        }

    }

    protected virtual void onElapse(){}

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
