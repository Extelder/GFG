using System;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerRing : MonoBehaviour
{
    private PlayerBinds _binds;

    private void Awake()
    {
        _binds = PlayerCharacter.Instance.Binds;
    }

    private void OnEnable()
    {
        _binds.Character.RingAbility.started += OnRingAbilityBindStarted;
        _binds.Character.RingAbility.canceled += OnRingAbilityBindCanceled;
        _binds.Character.RingAbility.performed += OnRingAbilityBindPerformed;

        _binds.Character.CancelAction.started += OnCancelActionStarted;
    }

    private void OnCancelActionStarted(InputAction.CallbackContext obj)
    {
        CancelAction();
    }

    protected abstract void CancelAction();

    protected virtual void OnRingAbilityBindPerformed(InputAction.CallbackContext obj)
    {
    }

    protected virtual void OnRingAbilityBindCanceled(InputAction.CallbackContext obj)
    {
    }

    protected virtual void OnRingAbilityBindStarted(InputAction.CallbackContext obj)
    {
    }


    private void OnDisable()
    {
        _binds.Character.RingAbility.started -= OnRingAbilityBindStarted;
        _binds.Character.RingAbility.canceled -= OnRingAbilityBindCanceled;
        _binds.Character.RingAbility.performed -= OnRingAbilityBindPerformed;

        CancelAction();
        _binds.Character.CancelAction.started -= OnCancelActionStarted;
    }
}