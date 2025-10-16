using System;
using UnityEngine;

public abstract class TimeAffectable : MonoBehaviour
{
    private void OnEnable()
    {
        TimeStop.TimeValueChanged += OnTimeValueChanged;
    }

    public abstract void OnTimeValueChanged(float value, bool affectPlayer);

    private void OnDisable()
    {
        TimeStop.TimeValueChanged -= OnTimeValueChanged;
    }
}