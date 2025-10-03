using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponMix : MonoBehaviour
{
    [SerializeField] private AudioSource _mixSound;
    
    [SerializeField] private ProjectileWeaponShoot _weaponShoot;
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _hammerAnimator;
    [SerializeField] private Animator _nailgunAnimator;

    private PlayerCharacter _character;

    private bool _cayout;

    private void Start()
    {
        _character = PlayerCharacter.Instance;
        _character.Binds.Character.MainShoot.started += OnMainShootStartedOld;
        _character.Binds.Character.SecondaryShoot.started += OnMainShootStartedOld;

        _character.Binds.Character.MainShoot.canceled += OnMainShootCanceledOld;
        _character.Binds.Character.SecondaryShoot.canceled += OnSecondaryShootCanceledOld;
    }

    private void OnSecondaryShootCanceledOld(InputAction.CallbackContext obj)
    {
        if (_cayout == false && _character.Binds.Character.MainShoot.IsPressed())
        {
            _cayout = true;
            _hammerAnimator.enabled = false;
            _nailgunAnimator.enabled = false;

            _animator.SetTrigger("Mix");
        }

        StopAllCoroutines();
    }


    private void OnMainShootCanceledOld(InputAction.CallbackContext obj)
    {
        if (_cayout == false &&
            _character.Binds.Character.SecondaryShoot.IsPressed())
        {
            _cayout = true;
            _hammerAnimator.enabled = false;
            _nailgunAnimator.enabled = false;

            _animator.SetTrigger("Mix");
        }

        StopAllCoroutines();
    }

    private void OnMainShootStartedOld(InputAction.CallbackContext obj)
    {
        _cayout = true;
        StopAllCoroutines();
        StartCoroutine(WaitingForCayout());
    }

    private IEnumerator WaitingForCayout()
    {
        _cayout = false;
        yield return new WaitForSeconds(.3f);
        _cayout = true;
    }

    public void AnimationEnd()
    {
        _hammerAnimator.enabled = true;
        _nailgunAnimator.enabled = true;
    }

    public void PerformMix()
    {
        _mixSound.Play();
    }

    private void OnDisable()
    {
        _character.Binds.Character.MainShoot.started -= OnMainShootStartedOld;
        _character.Binds.Character.SecondaryShoot.started -= OnMainShootStartedOld;

        _character.Binds.Character.MainShoot.canceled -= OnMainShootCanceledOld;
        _character.Binds.Character.SecondaryShoot.canceled -= OnSecondaryShootCanceledOld;
    }
}