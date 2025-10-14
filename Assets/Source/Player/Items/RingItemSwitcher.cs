using UnityEngine;

public class RingItemSwitcher : MonoBehaviour
{
    [field: SerializeField] public EquipItem CurrentItem { get; private set; }

    private EquipItem _previousItem;

    public void SwitchItem(EquipItem item)
    {
        if (CurrentItem == item)
            return;

        _previousItem = CurrentItem;
        if (CurrentItem != null)
        {
            CurrentItem.UnEquip();
        }

        Debug.Log(item);
        CurrentItem = item;
        CurrentItem.Equip();
    }

    public void ReturnToPreviousItem()
    {
        SwitchItem(_previousItem);
    }
}