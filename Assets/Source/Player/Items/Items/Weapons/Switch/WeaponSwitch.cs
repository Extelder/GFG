using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private Weapon _shotGun;
    [SerializeField] private Weapon _rifle;
    [SerializeField] private Weapon _rpg;
    [SerializeField] private Weapon _railgun;

    private GameObject _currentWeapon;

    private void OnEnable()
    {
    }

    private void OnWarmingEnd()
    {
        ShotGun();
    }

    private void OnDisable()
    {
    }

    public void UnlockShotGun()
    {
        _shotGun.Unlock();
        ChangeWeapon(_shotGun);
    }

    public void UnlockRifle()
    {
        _rifle.Unlock();
        ChangeWeapon(_rifle);
    }

    public void UnlockRPG()
    {
        _rpg.Unlock();
        ChangeWeapon(_rpg);
    }

    public void UnlockRailgun()
    {
        _railgun.Unlock();
        ChangeWeapon(_railgun);
    }

    public void ShotGun() => ChangeWeapon(_shotGun);
    public void Rifle() => ChangeWeapon(_rifle);
    public void RPG() => ChangeWeapon(_rpg);
    public void Railgun() => ChangeWeapon(_railgun);

    public void ChangeWeapon(Weapon weapon)
    {
        if (weapon.Unlocked == false)
            return;
        if (_currentWeapon == weapon.WeaponObject)
            return;
        _currentWeapon?.SetActive(false);
        _currentWeapon = weapon.WeaponObject;
        _currentWeapon.SetActive(true);
    }
}