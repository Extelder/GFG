using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventoryItem : MonoBehaviour
{
    [SerializeField] private EquipItem _inventoryEquipItem;

    [field: SerializeField] public int Id { get; private set; }

    public bool Unlocked { get; private set; } = true;


    [SerializeField] private string _actionName;

    public void Start()
    {
        PlayerCharacter.Instance.Binds.FindAction(_actionName, true).performed += OnEquipActionPerformed;
    }

    protected virtual void OnEquipActionPerformed(InputAction.CallbackContext obj)
    {
        PlayerCharacter.Instance.ItemSwitcher.SwitchItem(_inventoryEquipItem);
    }

    public void Unlock()
    {
        Unlocked = true;
    }

    private void OnEnable()
    {
        if (Unlocked)
            OnEnableVirtual();
    }

    protected virtual void OnEnableVirtual()
    {
    }

    protected virtual void OnDisableVirtual()
    {
    }

    private void OnDisable()
    {
        if (Unlocked)
        {
            OnDisableVirtual();
        }
    }

    private void OnDestroy()
    {
        PlayerCharacter.Instance.Binds.FindAction(_actionName, true).performed -= OnEquipActionPerformed;
    }
}