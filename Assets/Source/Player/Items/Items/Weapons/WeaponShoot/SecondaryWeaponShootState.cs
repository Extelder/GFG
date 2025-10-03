using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeaponShootState : DefaultWeaponShootState
{
    public override IEnumerator AnimationEndChecking()
    {
        while (true)
        {
            if (!CanShoot)
            {
                CanChanged = true;
                yield break;
            }

            if (PlayerCharacter.Instance.Binds.Character.SecondaryShoot.inProgress)
            {
                AlreadyShooting.Value = true;
                Animator.Shoot();
                yield break;
            }

            yield return new WaitForSeconds(0.02f);
        }
    }
}