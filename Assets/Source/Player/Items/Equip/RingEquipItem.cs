using UnityEngine;
using UnityEngine.InputSystem;

public class RingEquipItem : EquipItem
{
    [SerializeField] private RingItemSwitcher _ringItemSwitcher;

    protected override void OnEquipActionPerformed(InputAction.CallbackContext obj)
    {
        _ringItemSwitcher.SwitchItem(this);
    }
}