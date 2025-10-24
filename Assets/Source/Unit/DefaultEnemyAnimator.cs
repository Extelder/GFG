using UnityEngine;

public class DefaultEnemyAnimator : UnitAnimator
{
    [SerializeField] private string _moveBool;

    public void Move()
    {
        SetTrueAnimationBoolWithDisableOthers(_moveBool);
    }

    public override void DisableAllBools()
    {
        SetAnimationBool(_moveBool, false);
    }
}