using UnityEngine;

public class SwordCrouchAnimator : CrouchWeaponAnimator
{
    [SerializeField] private string _chargingBoolName;
    [SerializeField] private string _chargeAttackBoolName;

    public void Charging()
    {
        SetAnimationBool(_chargeAttackBoolName, false);
        SetTrueAnimationBoolWithDisableOthers(_chargingBoolName);
    }

    public void ChargeAttack()
    {
        SetAnimationBool(_chargingBoolName, false);
        SetTrueAnimationBoolWithDisableOthers(_chargeAttackBoolName);
    }
}