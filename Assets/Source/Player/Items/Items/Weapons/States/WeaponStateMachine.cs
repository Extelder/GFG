using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponStateMachine : StateMachine
{
    [SerializeField] private bool _mainShoot;

    [Header("States")] [SerializeField] private State _idle;
    [SerializeField] private WeaponShootState _shoot;


    public override void OnEnable()
    {
        base.OnEnable();

        if (!_mainShoot)
        {
            PlayerCharacter.Instance.Binds.Character.SecondaryShoot.started += OnMainShootPressedDown;
            PlayerCharacter.Instance.Binds.Character.SecondaryShoot.canceled += OnMainShootPressedUp;
            return;
        }

        PlayerCharacter.Instance.Binds.Character.MainShoot.started += OnMainShootPressedDown;
        PlayerCharacter.Instance.Binds.Character.MainShoot.canceled += OnMainShootPressedUp;
    }

    private void OnMainShootPressedDown(InputAction.CallbackContext obj)
    {
        StopAllCoroutines();

        StartCoroutine(WaitingForTakeUpToShoot());
    }

    private void OnMainShootPressedUp(InputAction.CallbackContext obj)
    {
        StopAllCoroutines();
        if (_shoot.CanShoot == false)
        {
            ChangeState(_idle);
        }

        StartCoroutine(TryingToExitShoot());
    }

    public void Idle()
    {
        ChangeState(_idle);
    }

    private IEnumerator WaitingForTakeUpToShoot()
    {
        while (true)
        {
            if (_shoot.CanShoot == false)
            {
                ChangeState(_idle);
                break;
            }

            if ( _shoot.CanShoot)
            {
                ChangeState(_shoot);
            }

            yield return new WaitForSeconds(0.05f);
        }
    }

    private IEnumerator TryingToExitShoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            ChangeState(_idle);
        }
    }

    public virtual void OnDisable()
    {
        if (!_mainShoot)
        {
            PlayerCharacter.Instance.Binds.Character.SecondaryShoot.started -= OnMainShootPressedDown;
            PlayerCharacter.Instance.Binds.Character.SecondaryShoot.canceled -= OnMainShootPressedUp;
            return;
        }

        PlayerCharacter.Instance.Binds.Character.MainShoot.started -= OnMainShootPressedDown;
        PlayerCharacter.Instance.Binds.Character.MainShoot.canceled -= OnMainShootPressedUp;
    }
}