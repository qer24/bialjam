using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossharAccuracy : MonoBehaviour
{
    [SerializeField] private PlayerInnacuracy inaccuracy;
    [SerializeField] private RectTransform crosshairOuter;
    [SerializeField] private float widthPerInaccuracy = 128;

    private float startWidth;

    private void Awake()
    {
        startWidth = crosshairOuter.sizeDelta.x;
    }

    private void Update()
    {
        var size = crosshairOuter.sizeDelta;
        size.x = startWidth + widthPerInaccuracy * inaccuracy.inaccuracy;
        crosshairOuter.sizeDelta = size;
    }
}
