using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : HealthBar
{
    [SerializeField] private Text _healthText;

    public override void OnHealthValueChanged(float value)
    {
        base.OnHealthValueChanged(value);
        _healthText.text = value.ToString();
    }
}