using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ReBindCollider : MonoBehaviour
{
    [SerializeField] private GameObject _interactButton;

    [SerializeField] private InputActionReference inputActionReference; //this is on the SO

    [SerializeField] private bool excludeMouse = true;
    [Range(0, 10)] [SerializeField] private int selectedBinding;
    [SerializeField] private InputBinding.DisplayStringOptions displayStringOptions;

    [Header("Binding Info - DO NOT EDIT")] [SerializeField]
    private InputBinding inputBinding;

    private int bindingIndex;

    private string actionName;

    [SerializeField] private Text rebindText;

    private InputActionRebindingExtensions.RebindingOperation _rebindingOperation;

    private void OnEnable()
    {
        if (inputActionReference != null)
        {
            InputManager.LoadBindingOverride(actionName);
            GetBindingInfo();
            UpdateUI();
        }
    }

    private void OnMouseEnter()
    {
        _interactButton.SetActive(true);

        if (inputActionReference != null)
        {
            InputManager.LoadBindingOverride(actionName);
            GetBindingInfo();
            UpdateUI();
        }

        DoRebind();

        InputManager.rebindComplete += UpdateUI;
        InputManager.rebindCanceled += UpdateUI;
    }


    private void OnMouseExit()
    {
        _interactButton.SetActive(false);

        _rebindingOperation.Cancel();

        InputManager.rebindComplete -= UpdateUI;
        InputManager.rebindCanceled -= UpdateUI;
    }

    private void OnDisable()
    {
        _rebindingOperation.Cancel();

        InputManager.rebindComplete -= UpdateUI;
        InputManager.rebindCanceled -= UpdateUI;
    }

    private void OnValidate()
    {
        if (inputActionReference == null)
            return;

        GetBindingInfo();
        UpdateUI();
    }

    private void GetBindingInfo()
    {
        if (inputActionReference.action != null)
            actionName = inputActionReference.action.name;

        if (inputActionReference.action.bindings.Count > selectedBinding)
        {
            inputBinding = inputActionReference.action.bindings[selectedBinding];
            bindingIndex = selectedBinding;
        }
    }

    private void UpdateUI()
    {
        if (rebindText != null)
        {
            if (Application.isPlaying)
            {
                rebindText.text = InputManager.GetBindingName(actionName, bindingIndex);
            }
            else
                rebindText.text = inputActionReference.action.GetBindingDisplayString(bindingIndex);
        }
    }

    private void DoRebind()
    {
        InputManager.StartRebind(actionName, bindingIndex, rebindText, excludeMouse, out _rebindingOperation);
    }
}