using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HorizontalSlider : MonoBehaviour
{
    [SerializeField] private float increment = 5;
    [SerializeField] private TextMeshProUGUI statusText;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();

        slider.onValueChanged.AddListener(UpdateStatusText);
        UpdateStatusText(slider.value);
    }

    private void UpdateStatusText(float value)
    {
        //string stringValue = value.ToString(CultureInfo.InvariantCulture);
        statusText.text = slider.wholeNumbers ? $"{value:F0}" : $"{value:F1}";
    }
    
    public void Add()
    {
        slider.value += increment;
    }

    public void Subtract()
    {
        slider.value -= increment;
    }
}