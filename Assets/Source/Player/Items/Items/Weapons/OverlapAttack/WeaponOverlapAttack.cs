using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOverlapAttack : WeaponShoot
{
    [field :SerializeField] public float Damage { get; set; }

    [field :SerializeField] public OverlapSettings OverlapSettings { get; private set; }

    private void OnEnable()
    {
        WeaponShootState.ShootPerformed += OnShootPerformed;
    }

    protected void Overlap()
    {
        OverlapSettings.Colliders = new Collider[10];
        OverlapSettings.Size = Physics.OverlapSphereNonAlloc(
            OverlapSettings.OverlapPoint.position + OverlapSettings.PositionOffset,
            OverlapSettings.SphereRadius, OverlapSettings.Colliders,
            OverlapSettings.SearchLayer);
    }

    public override void OnShootPerformed()
    {
        base.OnShootPerformed();
        CameraShakeInvoke();
        Overlap();
        foreach (var other in OverlapSettings.Colliders)
        {
            if (other == null)
                continue;
            if (other.TryGetComponent<IWeaponVisitor>(out IWeaponVisitor weaponVisitor))
            {
                weaponVisitor.Visit(this);
            }
        }
    }
    
    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(OverlapSettings.OverlapPoint.position, OverlapSettings.SphereRadius);
    }
    
    private void OnDisable()
    {
        WeaponShootState.ShootPerformed -= OnShootPerformed;
    }
}