using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrouchWeaponAnimator : WeaponAnimator
{
    [SerializeField] private string _crouchAnimatorBool;

    protected override void OnStartVirtual()
    {
        PlayerCharacter.Instance.Binds.Character.Crouch.started += OnCrochStarted;
        PlayerCharacter.Instance.Binds.Character.Crouch.canceled += OnCrochCanceled;
    }

    private void OnDisable()
    {
        PlayerCharacter.Instance.Binds.Character.Crouch.started -= OnCrochStarted;
        PlayerCharacter.Instance.Binds.Character.Crouch.canceled -= OnCrochCanceled;
    }

    private void OnCrochCanceled(InputAction.CallbackContext obj)
    {
        SetAnimationBool(_crouchAnimatorBool, false);
    }

    private void OnCrochStarted(InputAction.CallbackContext obj)
    {
        SetAnimationBool(_crouchAnimatorBool, true);
    }
}