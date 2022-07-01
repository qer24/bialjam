using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ResetKeybindsButton : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private InputRebindButton[] rebindButtons;
    
    public void ResetRebinds()
    {
        foreach (var map in inputActions.actionMaps)
        {
            map.RemoveAllBindingOverrides();
        }

        RebindsSaveLoad.ResetBinds();
        
        foreach (var button in rebindButtons)
        {
            button.UpdateUI();
        }
    }
}