using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class LampInventoryItem : PlayerInventoryItem
{
    [SerializeField] private GameObject _playerLight;
    [SerializeField] private GameObject _inventoryLight;

    private bool _active = false;

    private void Awake()
    {
        _active = Convert.ToBoolean(PlayerPrefs.GetInt("LampActive", 1));
    }

    protected override void OnEnableVirtual()
    {
        _playerLight.SetActive(_active);
        _inventoryLight.SetActive(_active);
        base.OnEnableVirtual();
    }

    protected override void OnEquipActionPerformed(InputAction.CallbackContext obj)
    {
        _active = !_active;
        PlayerPrefs.SetInt("LampActive", Convert.ToInt16(_active));
        Debug.Log(gameObject.activeInHierarchy);

        _inventoryLight.SetActive(_active);


        _playerLight.SetActive(_active);
        Debug.Log(_playerLight.activeSelf);
    }

    protected override void OnDisableVirtual()
    {
        _inventoryLight.SetActive(false);
        _playerLight.SetActive(_active);
        base.OnDisableVirtual();
    }
}