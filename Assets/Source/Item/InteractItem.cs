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
        _defaultMaterials = new Material[_meshRenderer.materials.Length];
        Array.Copy(_defaultMaterials, _meshRenderer.materials, _meshRenderer.materials.Length);

        for (int i = 0; i < _meshRenderer.materials.Length; i++)
        {
            _defaultMaterials[i] = _meshRenderer.materials[i];
        }


        _meshRenderer.materials = new Material[_defaultMaterials.Length + 1];
        for (int i = 0; i < _meshRenderer.materials.Length - 1; i++)
        {
            _meshRenderer.materials[i] = _defaultMaterials[i];
        }

        _meshRenderer.materials[_meshRenderer.materials.Length] = PlayerCharacter.Instance.InteractMaterial;
    }

    public void Lost()
    {
        if (!_detected)
            return;
        _detected = false;
        _meshRenderer.materials = _defaultMaterials;
    }
}