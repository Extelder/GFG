using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;


public class PlayerCharacter : MonoBehaviour
{
    [field: SerializeField] public Transform PlayerTransform { get; private set; }

    public static PlayerCharacter Instance { get; private set; }

    public PlayerBinds Binds { get; private set; }


    private void Awake()
    {
        if (!Instance)
        {
            Binds = InputManager.inputActions;
            Binds.Enable();

            Instance = this;
            return;
        }

        Debug.LogError("There`s one more PlayerCharacter");
    }


    private void OnDisable()
    {
        Binds.Dispose();
        Binds.Disable();
    }
}