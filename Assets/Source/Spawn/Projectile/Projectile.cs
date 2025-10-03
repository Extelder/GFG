using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class Projectile : PoolObject
{
    [field: SerializeField] public float Damage { get; private set; }
    [SerializeField] private float _speed;

    public event Action Exploded;

    [field: SerializeField] public Rigidbody Rigidbody { get; private set; }


    public virtual void Initiate(Vector3 targetPosition, bool useTargetPosition = true)
    {
        StopAllCoroutines();
        Rigidbody.linearVelocity = new Vector3(0, 0, 0);
        if (useTargetPosition)
            transform.LookAt(targetPosition, transform.forward);
        Rigidbody.AddForce(transform.forward * _speed, ForceMode.Impulse);
    }

    public virtual void Initiate(Vector3 targetPosition, float damage, bool useTargetPosition = true)
    {
        Damage = damage;
        Initiate(targetPosition, useTargetPosition);
    }

    protected virtual void VirtualOnTriggerEnter(Collider other)
    {
        if (other.material.bounciness >= 0.95f)
            return;
        if (other.TryGetComponent<Projectile>(out Projectile projectile))
            return;
        Rigidbody.linearVelocity = new Vector3(0, 0, 0);

        if (other.TryGetComponent<IWeaponVisitor>(out IWeaponVisitor visitor))
        {
            Accept(visitor);
        }

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        VirtualOnTriggerEnter(other);
    }


    public virtual void Accept(IWeaponVisitor visitor)
    {
        visitor.Visit(this);
    }
}