using System;
using UnityEngine;

public class PlayerHealUI : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    [SerializeField] private PlayerHealth _health;

    private void OnEnable()
    {
        _health.Healed += OnHealed;
    }

    private void OnHealed(float value)
    {
        _animator.Play("Heal");
        
    }

    private void OnDisable()
    {
        _health.Healed -= OnHealed;
    }
}
