using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class WeaponShootCameraShake : MonoBehaviour
{
    [SerializeField] private ShakePreset _shakePreset;

    [SerializeField] private WeaponShoot _weaponShoot;

    private void OnEnable()
    {
        _weaponShoot.CameraShake += ShootPerformed;
    }

    private void OnDisable()
    {
        _weaponShoot.CameraShake -= ShootPerformed;
    }

    private void ShootPerformed()
    {
       PlayerCharacter.Instance.Shaker.Shake(_shakePreset);
    }
}