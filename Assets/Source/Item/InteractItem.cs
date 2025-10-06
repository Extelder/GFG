using System;
using UnityEngine;

[Serializable]
public abstract class Item
{
    public abstract void Interact();
}

public class InteractItem : MonoBehaviour, IInteractable
{
    [SerializeField] private MeshRenderer _meshRenderer;

    [SerializeReference] [SerializeReferenceButton]
    public Item Item;

    private Material[] _defaultMaterials;

    private bool _detected;

    public void Interact()
    {
        Item.Interact();
    }

    public void Detected()
    {
        if (_detected)
            return;

        _detected = true;

        _defaultMaterials = _meshRenderer.materials;

        Material[] mats = new Material[_defaultMaterials.Length + 1];
        for (int i = 0; i < _defaultMaterials.Length; i++)
        {
            mats[i] = _defaultMaterials[i];
        }

        mats[mats.Length - 1] = PlayerCharacter.Instance.InteractMaterial;

        _meshRenderer.materials = mats;
    }

    public void Lost()
    {
        if (!_detected)
            return;
        _detected = false;
        _meshRenderer.materials = _defaultMaterials;
    }
}