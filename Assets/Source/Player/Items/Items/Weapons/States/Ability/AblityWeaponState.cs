using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AblityWeaponState : State
{
    [field: SerializeField] public AbilityWeaponAnimator Animator { get; private set; }

    public event Action AbilityUsed;

    public override void Enter()
    {
        CanChanged = false;
        Animator.Ability();
    }

    public void PerformAbility()
    {
        Debug.LogError("Preformed");
        AbilityUsed?.Invoke();
    }

    public void AnimationEnd()
    {
        CanChanged = true;
    }
}