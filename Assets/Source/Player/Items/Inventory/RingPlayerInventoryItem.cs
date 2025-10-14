using UnityEngine;
using UnityEngine.InputSystem;

public class RingPlayerInventoryItem : PlayerInventoryItem
{
    [SerializeField] private RingItemSwitcher _ringItemSwitcher;

    protected override void OnEquipActionPerformed(InputAction.CallbackContext obj)
    {
        _ringItemSwitcher.SwitchItem(_inventoryEquipItem);

        if (gameObject.activeInHierarchy)
            PlayerCharacter.Instance.ItemSwitcher.ReturnToPreviousItem();
    }

    protected override void OnMouseDown()
    {
        if (_rebind.Selected)
            return;
        _ringItemSwitcher.SwitchItem(_inventoryEquipItem);

        if (gameObject.activeInHierarchy)
            PlayerCharacter.Instance.ItemSwitcher.ReturnToPreviousItem();
    }
}