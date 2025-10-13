using System;
using UnityEngine;

public class OnMouseEnterExitSetActive : MonoBehaviour
{
    [SerializeField] private GameObject _gameObject;

    private void OnMouseEnter()
    {
        _gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        _gameObject.SetActive(false);
    }
}