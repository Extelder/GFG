using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class Rapier : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _attackLayers;
    [SerializeField] private Pool _defaultHitPool;

    [SerializeField] private KeyCode _attackKey;
    [SerializeField] private string _attackBoolAnimationName;
    [SerializeField] private string _performAttackBoolAnimationName;
    [SerializeField] private string _slashAttackBoolAnimationName = "Slash";
    [SerializeField] private string _obstacleAttackBoolAnimationName;
    [SerializeField] private AudioSource _chargeReady;
    [SerializeField] private AudioSource _performAttackSound;
    [SerializeField] private AudioSource _flashSound;
    [SerializeField] private ParticleSystem _chargeParticle;
    [field: SerializeField] public int Damage { get; private set; }

    private Animator _animator;
    public event Action ChargingStart;
    public event Action ChargingEnd;

    private bool _charged;

    private bool _charging;

    public RaycastHit Hit;


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(_camera.position, _camera.forward * _attackRange);
    }

    private void OnEnable()
    {
        PlayerCharacter.Instance.Binds.Character.MainShoot.started += OnMainShootStarted;
        PlayerCharacter.Instance.Binds.Character.MainShoot.canceled += OnMainShootCanceled;
    }

    private void OnMainShootCanceled(InputAction.CallbackContext obj)
    {
        StopAllCoroutines();

        _charging = false;

        ChargingEnd?.Invoke();
        _animator.SetBool(_attackBoolAnimationName, false);
        if (_charged)
            _animator.SetBool(_performAttackBoolAnimationName, true);
        else
        {
            _animator.SetBool(_slashAttackBoolAnimationName, true);
        }
    }

    private void OnMainShootStarted(InputAction.CallbackContext obj)
    {
        _charging = true;
        _animator.SetBool(_obstacleAttackBoolAnimationName, false);

        StopAllCoroutines();
        StartCoroutine(StartAttackingCheck());

        _charged = false;
    }

    private void OnDisable()
    {
        PlayerCharacter.Instance.Binds.Character.MainShoot.started -= OnMainShootStarted;
        PlayerCharacter.Instance.Binds.Character.MainShoot.canceled -= OnMainShootCanceled;
    }

    private IEnumerator StartAttackingCheck()
    {
        yield return new WaitForSeconds(0.1f);
        _animator.SetBool(_attackBoolAnimationName, true);
    }

    public void SlashAttack()
    {
        _animator.SetBool(_slashAttackBoolAnimationName, false);
    }

    public void FleshSound()
    {
        //       _flashSound?.Play();
    }

    public void ChargeStart()
    {
        ChargingStart?.Invoke();
    }

    public void ChargedParticle()
    {
        if (_charging == true)
            _chargeParticle?.Play();
    }

    public void ChargeAudio()
    {
        //      if (_charging == true)
//            _chargeReady?.Play();
    }

    public void ChargeReady()
    {
        _charged = true;
    }

    public void PerformAttack()
    {
        _charged = false;
        _animator.SetBool(_performAttackBoolAnimationName, false);

        if (Physics.Raycast(_camera.position, _camera.forward, out Hit, _attackRange, _attackLayers))
        {
            if (Hit.collider.TryGetComponent<IWeaponVisitor>(out IWeaponVisitor visitor))
            {
            }
            else
            {
                PoolObject instance = _defaultHitPool.GetFreeElement(Hit.point);

                instance.transform.eulerAngles = Hit.normal;

                _animator.SetBool(_obstacleAttackBoolAnimationName, true);
            }
        }
    }
}