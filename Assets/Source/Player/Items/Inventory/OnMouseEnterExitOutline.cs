using System;
using UnityEngine;

public class OnMouseEnterExitOutline : MonoBehaviour
{
    [SerializeField] private Outline[] _outlines;

    private void OnMouseEnter()
    {
        for (int i = 0; i < _outlines.Length; i++)
        {
            _outlines[i].enabled = true;
        }
    }

    private void OnMouseExit()
    {
        for (int i = 0; i < _outlines.Length; i++)
        {
            _outlines[i].enabled = false;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _outlines.Length; i++)
        {
            _outlines[i].enabled = false;
        }
    }
}