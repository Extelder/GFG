using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    [SerializeField] private PlayerHealth _health;

    private void OnEnable()
    {
        _health.HealthValueChanged += OnHealthValueChanged;
    }

    private void OnHealthValueChanged(float value)
    {
        _text.text = value.ToString();
    }

    private void OnDisable()
    {
        _health.HealthValueChanged -= OnHealthValueChanged;
    }
}