using System;
using UnityEngine;

public class PlayerItemSwitcher : MonoBehaviour
{
    [SerializeField] private Animator _switchAnimator;

    [field: SerializeField] public EquipItem CurrentItem { get; private set; }

    private EquipItem _itemToSwitch;

    public void SwitchItem(EquipItem item)
    {
        _switchAnimator.SetBool("Hide", true);

        _itemToSwitch = item;
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