using UnityEngine;

public class UnitHitBox : MonoBehaviour, IWeaponVisitor
{
    [SerializeField] private GameObject _hitEffect;

    public void Visit(WeaponShoot weaponShoot)
    {
    }

    public void Visit(RaycastWeaponShoot raycastWeaponShoot, RaycastHit hit)
    {
    }

    public void Visit(WeaponOverlapAttack weaponOverlapAttack, RaycastHit hit)
    {
        Instantiate(_hitEffect, hit.point, Quaternion.identity);
    }

    public void Visit(Projectile projectile)
    {
    }
}