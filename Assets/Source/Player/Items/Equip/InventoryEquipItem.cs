using EvolveGames;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryEquipItem : EquipItem
{
    [SerializeField] private HandsSmooth _handsSmooth;

    public override void Equip()
    {
        base.Equip();
        _handsSmooth.enabled = false;
    }

    public override void UnEquip()
    {
        base.UnEquip();
        _handsSmooth.enabled = true;
    }

    protected override void OnEquipActionPerformed(InputAction.CallbackContext obj)
    {
        if (PlayerCharacter.Instance.ItemSwitcher.CurrentItem == this)
        {
            PlayerCharacter.Instance.ItemSwitcher.ReturnToPreviousItem();
            return;
        }

        base.OnEquipActionPerformed(obj);
    }
}