using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

public class DefaultWeaponShootState : WeaponShootState
{
    [SerializeField] private float _secondsToCharge;

    public event Action ShootPerformed;

    public ReactiveProperty<bool> AlreadyShooting = new ReactiveProperty<bool>();

    private SwordCrouchAnimator _swordCrouchAnimator;

    private Coroutine _waitingForChargingCoroutine;

    private void Start()
    {
        _swordCrouchAnimator = (SwordCrouchAnimator) Animator;
    }

    public override void Enter()
    {
        CanChanged = false;
        if (_waitingForChargingCoroutine != null)
        {
            StopCoroutine(_waitingForChargingCoroutine);
        }

        _waitingForChargingCoroutine = StartCoroutine(WaitingForCharging());
    }

    private IEnumerator WaitingForCharging()
    {
        float currentSeconds = _secondsToCharge;
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            currentSeconds -= 0.02f;
            if (PlayerCharacter.Instance.Binds.Character.MainShoot.IsPressed())
            {
                if (currentSeconds <= 0)
                {
                    _swordCrouchAnimator.Charging();
                    yield break;
                }
            }
            else
            {
                Animator.Shoot();
                yield break;
            }
        }
    }

    private void OnDisable()
    {
        PlayerCharacter.Instance.Binds.Character.MainShoot.canceled -= OnAfterAnimationEndCheckingPerformed;
    }

    public void ChargeReady()
    {
        _swordCrouchAnimator.ChargeAttack();
    }

    public void PerformShoot()
    {
        ShootPerformed?.Invoke();
    }

    public void AnimationEndStartChecking()
    {
        AlreadyShooting.Value = false;
        StopAllCoroutines();
        StartCoroutine(AnimationEndChecking());
    }

    public void AnimationEndWithoutChecking()
    {
        StopAllCoroutines();
        AlreadyShooting.Value = false;
        CanChanged = true;
    }

    public void AnimationEndStopChecking()
    {
        StopAllCoroutines();

        PlayerCharacter.Instance.Binds.Character.MainShoot.canceled -= OnAfterAnimationEndCheckingPerformed;
        if (AlreadyShooting.Value)
            return;

        CanChanged = true;
    }

    public override void Exit()
    {
        base.Exit();
        StopAllCoroutines();
        AlreadyShooting.Value = false;
        PlayerCharacter.Instance.Binds.Character.MainShoot.canceled -= OnAfterAnimationEndCheckingPerformed;
    }

    public virtual IEnumerator AnimationEndChecking()
    {
        float waitTime = 0;
        PlayerCharacter.Instance.Binds.Character.MainShoot.canceled += OnAfterAnimationEndCheckingPerformed;
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            waitTime += 0.02f;
            if (!CanShoot)
            {
                CanChanged = true;
                yield break;
            }

            if (PlayerCharacter.Instance.Binds.Character.MainShoot.inProgress)
            {
                if (waitTime >= _secondsToCharge)
                {
                    AlreadyShooting.Value = false;
                    _swordCrouchAnimator.Charging();
                    PlayerCharacter.Instance.Binds.Character.MainShoot.performed -=
                        OnAfterAnimationEndCheckingPerformed;
                    yield break;
                }
            }
            else
            {
                AlreadyShooting.Value = false;
                CanChanged = true;
                PlayerCharacter.Instance.Binds.Character.MainShoot.canceled -= OnAfterAnimationEndCheckingPerformed;
                StopAllCoroutines();
                yield break;
            }
        }
    }

    private void OnAfterAnimationEndCheckingPerformed(InputAction.CallbackContext obj)
    {
        PlayerCharacter.Instance.Binds.Character.MainShoot.performed -= OnAfterAnimationEndCheckingPerformed;
        Debug.LogError("After");
        StopAllCoroutines();
        AlreadyShooting.Value = true;
        Animator.Shoot();
    }
}