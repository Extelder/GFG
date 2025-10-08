using UnityEngine;

public class LampInventoryItem : PlayerInventoryItem
{
    [SerializeField] private GameObject _playerLight;

    protected override void OnEnableVirtual()
    {
        _playerLight.SetActive(false);
        base.OnEnableVirtual();
    }

    protected override void OnDisableVirtual()
    {
        _playerLight.SetActive(true);
        base.OnDisableVirtual();
    }
}