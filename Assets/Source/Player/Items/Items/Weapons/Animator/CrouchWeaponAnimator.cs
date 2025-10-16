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

    private void OnEnable()
    {
        PlayerCharacter.Instance.Binds.Character.Crouch.started += OnCrochStarted;
        PlayerCharacter.Instance.Binds.Character.Crouch.canceled += OnCrochCanceled;
        if (PlayerCharacter.Instance.Binds.Character.Crouch.IsPressed() ||
            PlayerCharacter.Instance.PlayerController.isCrough)
        {
            OnCrochStarted(new InputAction.CallbackContext());
        }
    }

    private void Update()
    {
        if (PlayerCharacter.Instance.PlayerController.isCrough)
        {
            SetAnimationBool(_crouchAnimatorBool, true);
        }
        else
        {
            SetAnimationBool(_crouchAnimatorBool, false);
        }
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