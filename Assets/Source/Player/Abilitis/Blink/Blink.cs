using System;
using UniRx;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] private Transform _blinkPoint;
    [SerializeField] private Transform _crouchPoint;
    [SerializeField] private OverlapSettings _overlapSettings;
    [SerializeField] private OverlapSettings _overlapDownSettings;

    public Vector3 TargetPoint { get; private set; }

    private CompositeDisposable _disposable = new CompositeDisposable();

    private void OnEnable()
    {
        _disposable?.Clear();
    }

    private void Update()
    {
        Collider[] collidersUpper = new Collider[_overlapSettings.Size];
        Physics.OverlapSphereNonAlloc(_overlapSettings.OverlapPoint.position, _overlapSettings.SphereRadius,
            collidersUpper, _overlapSettings.SearchLayer);
        for (int q = 0; q < collidersUpper.Length; q++)
        {
            if (collidersUpper[q] == null)
                continue;
            Debug.Log("Upper Detected");
            Collider[] collidersDown = new Collider[_overlapDownSettings.Size];

            Physics.OverlapSphereNonAlloc(_overlapDownSettings.OverlapPoint.position, _overlapDownSettings.SphereRadius,
                collidersDown, _overlapDownSettings.SearchLayer);
            for (int i = 0; i < collidersDown.Length; i++)
            {
                if (collidersDown[i] != null)
                    return;
            }

            Debug.Log("Down Not Detected");
            TargetPoint = _crouchPoint.position;
            return;
        }


        TargetPoint = _blinkPoint.position;
    }

    private void OnDisable()
    {
        _disposable?.Clear();
    }

    public void Blinked()
    {
        Collider[] collidersUpper = new Collider[_overlapSettings.Size];
        Physics.OverlapSphereNonAlloc(_overlapSettings.OverlapPoint.position, _overlapSettings.SphereRadius,
            collidersUpper, _overlapSettings.SearchLayer);
        for (int q = 0; q < collidersUpper.Length; q++)
        {
            if (collidersUpper[q] == null)
                continue;
            Debug.Log("Upper Detected");
            Collider[] collidersDown = new Collider[_overlapDownSettings.Size];

            Physics.OverlapSphereNonAlloc(_overlapDownSettings.OverlapPoint.position, _overlapDownSettings.SphereRadius,
                collidersDown, _overlapDownSettings.SearchLayer);
            for (int i = 0; i < collidersDown.Length; i++)
            {
                if (collidersDown[i] != null)
                    return;
            }

            Debug.Log("Down Not Detected");
            TargetPoint = _crouchPoint.position;
            Debug.Log("Crouch");
            PlayerCharacter.Instance.PlayerController.Crouch();
            PlayerCharacter.Instance.Teleport(TargetPoint);

            gameObject.SetActive(false);
            return;
        }

        TargetPoint = _blinkPoint.position;

        PlayerCharacter.Instance.Teleport(TargetPoint);
        gameObject.SetActive(false);
    }
}