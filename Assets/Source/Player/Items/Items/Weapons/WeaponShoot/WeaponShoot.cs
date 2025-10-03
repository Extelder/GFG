using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponShoot : RaycastBehaviour
{
    [field: SerializeField] public DefaultWeaponShootState WeaponShootState { get; private set; }

    [field: SerializeField] public RaycastWeaponItem Weapon { get; private set; }

    public Vector3 CurrentShootOffset;

    public Action BulletSpawned;
    public event Action ShootPerformed;
    public event Action CameraShake;

    private void OnEnable()
    {
        WeaponShootState.ShootPerformed += OnShootPerformed;
        OnEnableVirtual();
    }

    public virtual void OnEnableVirtual()
    {}

    private void OnDisable()
    {
        WeaponShootState.ShootPerformed -= OnShootPerformed;
        OnDisableVirtual();
    }

    public virtual void OnDisableVirtual()
    {}

    public virtual void OnShootPerformed()
    {
        ShootPerformed?.Invoke();
    }

    public virtual void Accept(IWeaponVisitor visitor)
    {
        visitor.Visit(this);
    }

    public void CameraShakeInvoke()
    {
        CameraShake?.Invoke();
    }
}