using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEndUnityEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent Event;

    private void Start()
    {
        EnemyManager.instance.OnEnd += () => Event?.Invoke();
    }
}
