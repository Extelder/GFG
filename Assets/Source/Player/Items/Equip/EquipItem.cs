using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipItem : MonoBehaviour
{
    [SerializeField] private bool _canSpellHand;
    [SerializeField] private GameObject _gameObject;

    [SerializeField] private string _actionName;

    [field: SerializeField] public int Id { get; private set; }

    public static event Action Equiped;
    public static event Action UnEquiped;

    public void Start()
    {
        PlayerCharacter.Instance.Binds.FindAction(_actionName, true).performed += OnEquipActionPerformed;
    }

    protected virtual void OnEquipActionPerformed(InputAction.CallbackContext obj)
    {
        PlayerCharacter.Instance.ItemSwitcher.SwitchItem(this);
    }

    public virtual void UnEquip()
    {
        if (_canSpellHand)
            UnEquiped?.Invoke();
        _gameObject?.SetActive(false);
    }

    public virtual void Equip()
    {
        if (_canSpellHand)
            Equiped?.Invoke();
        _gameObject?.SetActive(true);
    }

    private void OnDisable()
    {
        PlayerCharacter.Instance.Binds.FindAction(_actionName, true).performed -= OnEquipActionPerformed;
    }
}