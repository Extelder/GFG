using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public event Action Dead;
    public event Action<float> PlayerDamaged;

    public bool IsDead { get; private set; }

    public override void TakeDamage(float value)
    {

        base.TakeDamage(value);
        if (value > 100)
        {
            PlayerDamaged?.Invoke(100);
            return;
        }

        PlayerDamaged?.Invoke(value);
    }

    public override void Death()
    {
        IsDead = true;
        Dead?.Invoke();
    }
}