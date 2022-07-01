using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputRebindButton : MonoBehaviour
{
    [SerializeField] private InputActionReference actionReference;
    [SerializeField] private GameObject waitingForInputPanel;

    private TextMeshProUGUI buttonDisplayText;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    private Dictionary<string, InputAction> inputMap;

    private static Action OnBindingsChanged;

    private void Start()
    {
        buttonDisplayText = GetComponentInChildren<TextMeshProUGUI>();
        UpdateUI();
        RefreshInputMap();
    }

    private void OnEnable()
    {
        OnBindingsChanged += UpdateUI;
    }

    private void OnDisable()
    {
        OnBindingsChanged -= UpdateUI;
    }

    private void RefreshInputMap()
    {
        inputMap = new Dictionary<string, InputAction>();
        foreach (var action in actionReference.action.actionMap.actions.Where(action => action != actionReference.action))
        {
            inputMap.Add(action.bindings[0].effectivePath, action);
        }
    }

    public void StartRebinding()
    {
        waitingForInputPanel.SetActive(true);

        actionReference.action.Disable();

        var previousBinding = actionReference.action.bindings[0];
        rebindingOperation = actionReference.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation => FinishRebinding(previousBinding))
            .Start();
    }

    private void FinishRebinding(InputBinding oldBinding)
    {
        waitingForInputPanel.SetActive(false);
        rebindingOperation.Dispose();

        var newBinding = actionReference.action.bindings[0].effectivePath;

        RefreshInputMap();
        if (inputMap.ContainsKey(newBinding))
        {
            inputMap[newBinding].Disable();
            inputMap[newBinding].ApplyBindingOverride(oldBinding.effectivePath);
            inputMap[newBinding].Enable();
        }

        actionReference.action.Enable();
        
        OnBindingsChanged?.Invoke();
    }

    public void UpdateUI()
    {
        //var bindingIndex = actionReference.action.GetBindingIndexForControl(actionReference.action.controls[0]);
        //var readableString = InputControlPath.ToHumanReadableString(actionReference.action.bindings[bindingIndex].effectivePath, InputControlPath.HumanReadableStringOptions.OmitDevice);
        var readableString = actionReference.action.bindings[0].ToDisplayString();
        buttonDisplayText.text = readableString;
    }
}   