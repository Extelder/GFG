using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAnimator : UnitAnimator
{
    [SerializeField] private string _moveAnimationTriggerName;

    private void Start()
    {
        Idle();
    }

    public override void DisableAllBools()
    {
        SetAnimationBool(_moveAnimationTriggerName, false);
    }

    public void Idle()
    {
        DisableAllBools();
    }

    public void Move()
    {
        SetAnimationTrigger(_moveAnimationTriggerName);
    }
}