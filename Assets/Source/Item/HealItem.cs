using UnityEngine;

public class HealItem : Item
{
    [SerializeField] private GameObject _gameObject;
    [SerializeField] private float _value;

    public override void Interact()
    {
        PlayerCharacter.Instance.Health.Heal(_value);
        MonoBehaviour.Destroy(_gameObject);
    }
}