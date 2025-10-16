using System;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    public float CurrentScale { get; private set; }
    public bool AffectingPlayer { get; private set; }

    public static event Action<float, bool> TimeValueChanged;

    public void SetTimeValue(float newValue, bool affectPlayer)
    {
        CurrentScale = newValue;
        AffectingPlayer = affectPlayer;
        TimeValueChanged?.Invoke(CurrentScale, AffectingPlayer);
    }
}