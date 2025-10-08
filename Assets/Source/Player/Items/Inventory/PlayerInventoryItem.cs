using System;
using UnityEngine;

public class PlayerInventoryItem : MonoBehaviour
{
    [field: SerializeField] public int Id { get; private set; }

    public bool Unlocked { get; private set; } = true;


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
            OnDisableVirtual();
    }
}