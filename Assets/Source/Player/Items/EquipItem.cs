using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class EquipItem : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;
    
    [SerializeField] private string _actionName;

    [field: SerializeField] public int Id { get; private set; }

    public void Start()
    {
        PlayerCharacter.Instance.Binds.FindAction(_actionName, true).performed += OnEquipACtionPerformed;
    }

    private void OnEquipACtionPerformed(InputAction.CallbackContext obj)
    {
        PlayerCharacter.Instance.ItemSwitcher.SwitchItem(this);
    }

    public virtual void UnEquip()
    {
        _gameObject.SetActive(false);
    }

    public virtual void Equip()
    {
        _gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        PlayerCharacter.Instance.Binds.FindAction(_actionName, true).performed -= OnEquipACtionPerformed;
    }
}