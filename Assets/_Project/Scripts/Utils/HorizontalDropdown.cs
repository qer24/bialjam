using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HorizontalDropdown : MonoBehaviour
{
    private TMP_Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
    }

    public void Next()
    {
        if (dropdown.value == dropdown.options.Count - 1)
        {
            dropdown.value -= 999;
        }
        else
        {
            dropdown.value += 1;
        }
    }

    public void Previous()
    {
        if (dropdown.value == 0)
        {
            dropdown.value += 999;
        }
        else
        {
            dropdown.value -= 1;
        }
    }
}