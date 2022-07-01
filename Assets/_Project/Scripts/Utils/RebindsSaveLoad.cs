using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindsSaveLoad : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputs;
    private const string rebindsStr = "rebinds";
    
    private void OnEnable()
    {
        var rebinds = PlayerPrefs.GetString(rebindsStr);
        if (!rebinds.IsNullOrWhitespace())
        {
            inputs.LoadBindingOverridesFromJson(rebinds);
        }
    }

    private void OnDisable()
    {
        var rebinds = inputs.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(rebindsStr, rebinds);
    }

    public static void ResetBinds()
    {
        PlayerPrefs.DeleteKey(rebindsStr);
    }
}