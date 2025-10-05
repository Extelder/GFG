using System;
using UnityEngine;

[Serializable]
public abstract class Item
{
    public abstract void Interact();
}

public class InteractItem : MonoBehaviour, IInteractable
{
    [SerializeReference] [SerializeReferenceButton]
    public Item Item;

    public void Interact()
    {
        Item.Interact();
    }
}