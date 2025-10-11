using System;
using UnityEngine;

public class AbilityArmEquipItem : MonoBehaviour
{
    [SerializeField] private GameObject _abilityArm;
    
    private void OnEnable()
    {
        EquipItem.Equiped += OnItemEquiped;
        EquipItem.UnEquiped += OnItemUnEquiped;
    }

    private void OnItemEquiped()
    {
        _abilityArm.SetActive(true);
    }

    private void OnItemUnEquiped()
    {
        _abilityArm.SetActive(false);
    }

    private void OnDisable()
    {
        EquipItem.Equiped -= OnItemEquiped;
        EquipItem.UnEquiped -= OnItemUnEquiped;
    }
}