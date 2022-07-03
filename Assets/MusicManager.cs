using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    [SerializeField] private EventReference music;
    
    public override void Awake()
    {
        transform.SetParent(null);
        
        keepAlive = true;
        base.Awake();
    }

    private void Start()
    {
        music.Play();
    }
}