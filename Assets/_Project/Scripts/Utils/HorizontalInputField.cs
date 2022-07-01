using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class HorizontalInputField : MonoBehaviour
{
    [SerializeField] private int increment = 1;
    [SerializeField] private Vector2 minMaxValue;
    private TMP_InputField inputField;

    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onEndEdit.AddListener(ClampValue);
    }

    private void ClampValue(string value)
    {
        var current = int.Parse(inputField.text, CultureInfo.InvariantCulture);
        inputField.text = Mathf.Clamp(current, minMaxValue.x, minMaxValue.y).ToString(CultureInfo.InvariantCulture);
    }
    
    public void Add()
    {
        var current = int.Parse(inputField.text, CultureInfo.InvariantCulture);
        inputField.text = Mathf.Clamp(current + increment, minMaxValue.x, minMaxValue.y).ToString(CultureInfo.InvariantCulture);
    }

    public void Subtract()
    {
        var current = int.Parse(inputField.text, CultureInfo.InvariantCulture);
        inputField.text = Mathf.Clamp(current - increment, minMaxValue.x, minMaxValue.y).ToString(CultureInfo.InvariantCulture);
    }
}