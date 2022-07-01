using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ScaleToMiddleSlider : MonoBehaviour
{
    [Range(0, 1), SerializeField] private float fillValue;

    public float Value
    {
        get => fillValue;
        set
        {
            fill.localScale = transform.localScale.WithX(fillValue);
            fillValue = Mathf.Clamp01(value);
        }
    }
    
    [SerializeField] private Transform fill;

    private void OnValidate()
    {
        if(fill == null) return;
        fill.localScale = transform.localScale.WithX(fillValue);
    }
}