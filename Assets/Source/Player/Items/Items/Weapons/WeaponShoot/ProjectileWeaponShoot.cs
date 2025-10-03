using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum ShootType
{
    Rifle,
    Shotgun
}

public class ProjectileWeaponShoot : WeaponShoot
{
    [SerializeField] private RaycastWeaponItem _weaponItem;
    
    [SerializeField] private AudioSource _shotGunSound;
    [SerializeField] private WeaponStateMachine _weaponStateMachine;
    [SerializeField] private DefaultWeaponShootState _defaultWeaponShootState;

    [SerializeField] private Transform _muzzle;

    [SerializeField] private float _defaultDamage;
    private Pool _currentPool;

    private void Start()
    {
        Initiate();
    }

    public virtual void Initiate()
    {
        _currentPool = Pools.Instance.DefaultProjectilePool;
    }

    public void ResetWeapon()
    {
        _weaponStateMachine.StopAllCoroutines();
        _defaultWeaponShootState.StopAllCoroutines();
    }


    public override void OnShootPerformed()
    {
        base.OnShootPerformed();

        CameraShakeInvoke();

        BulletSpawned?.Invoke();

        for (int i = 0; i < _weaponItem.HitsPerShot; i++)
        {
            BulletSpawned?.Invoke();
            Vector3 random = Random.insideUnitSphere * _weaponItem.RandomRangeMultiplayer;

            Vector3 direction = Camera.position + random + Camera.forward * Range;
            if (GetHitCollider(out Collider collider2))
            {
                direction = GetRaycastHit().point + random;
            }

            Projectile projectileShotGun = _currentPool
                .GetFreeElement(_muzzle.position + random,
                    Quaternion.FromToRotation(_muzzle.position, direction))
                .GetComponent<Projectile>();
            projectileShotGun.Initiate(direction, _defaultDamage, true);
        }
    }
}