using UnityEngine;

public class HealItem : Item
{
    [SerializeField] private float _value;
    
    public override void Interact()
    {
        PlayerCharacter.Instance.Health.Heal(_value);
    }
}
