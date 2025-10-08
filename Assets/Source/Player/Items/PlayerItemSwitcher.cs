using System;
using UnityEngine;

public class PlayerItemSwitcher : MonoBehaviour
{
    [SerializeField] private Animator _switchAnimator;

    [field: SerializeField] public EquipItem CurrentItem { get; private set; }

    private EquipItem _itemToSwitch;

    private EquipItem _previousItem;

    public void SwitchItem(EquipItem item)
    {
        if (CurrentItem == item || item == _itemToSwitch)
            return;

        _switchAnimator.SetBool("Hide", true);

        _previousItem = CurrentItem;
        _itemToSwitch = item;
    }

    public void ReturnToPreviousItem()
    {
        SwitchItem(_previousItem);
    }

    public void HideAnimationEnd()
    {
        if (_itemToSwitch == null)
            return;

        _switchAnimator.SetBool("Hide", false);
        if (CurrentItem != null)
        {
            CurrentItem.UnEquip();
        }

        CurrentItem = _itemToSwitch;
        CurrentItem.Equip();
    }
}