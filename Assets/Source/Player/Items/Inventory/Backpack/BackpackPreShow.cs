using System;
using UnityEngine;

public class BackpackPreShow : MonoBehaviour
{
    [SerializeField] private Animator _backpackAnimator;

    private void OnMouseEnter()
    {
        _backpackAnimator.SetBool("PreShow", true);
    }

    private void OnMouseExit()
    {
        _backpackAnimator.SetBool("PreShow", false);
    }
}