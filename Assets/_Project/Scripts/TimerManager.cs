using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : Singleton<TimerManager>
{
    public float timer;
    private bool stopped = false;

    public override void Awake()
    {
        keepAlive = false;
        base.Awake();
    }

    private void Update()
    {
        if (!stopped) timer += Time.deltaTime;
    }

    public void Reset()
    {
        timer = 0;
        stopped = false;
    }

    public void Stop()
    {
        stopped = true;
    }
}
