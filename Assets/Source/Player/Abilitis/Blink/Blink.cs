using System;
using UniRx;
using UnityEngine;

public class Blink : MonoBehaviour
{
    [SerializeField] private float _offsetDistance = 0.05f;
    [SerializeField] private float _height = 1f;

    [SerializeField] private TeleportRing _teleportRing;

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
        _overlapDownSettings.OverlapPoint.transform.rotation = Quaternion.Euler(0, 0, 0);
        _overlapDownSettings.OverlapPoint.transform.position =
            _teleportRing.Hit.point + _teleportRing.Hit.normal * _offsetDistance;

        Collider[] collidersUpper = new Collider[_overlapSettings.Size];

        float halfHeightUpper = Mathf.Max(0, (_height * 0.5f) - _overlapSettings.SphereRadius);
        Vector3 upDirUpper = _overlapSettings.OverlapPoint.up;

        Vector3 point1Upper = _overlapSettings.OverlapPoint.position + upDirUpper * halfHeightUpper;
        Vector3 point2Upper = _overlapSettings.OverlapPoint.position - upDirUpper * halfHeightUpper;

        int upperCount = Physics.OverlapCapsuleNonAlloc(
            point1Upper,
            point2Upper,
            _overlapSettings.SphereRadius,
            collidersUpper,
            _overlapSettings.SearchLayer
        );

        for (int q = 0; q < upperCount; q++)
        {
            if (collidersUpper[q] == null)
                continue;

            Debug.Log("Upper Detected");
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
        _overlapDownSettings.OverlapPoint.transform.rotation = Quaternion.Euler(0, 0, 0);
        _overlapDownSettings.transform.position = _teleportRing.Hit.point + _teleportRing.Hit.normal * _offsetDistance;

        Collider[] collidersUpper = new Collider[_overlapSettings.Size];

        float halfHeightUpper = Mathf.Max(0, (_height * 0.5f) - _overlapSettings.SphereRadius);
        Vector3 upDirUpper = _overlapSettings.OverlapPoint.up;

        Vector3 point1Upper = _overlapSettings.OverlapPoint.position + upDirUpper * halfHeightUpper;
        Vector3 point2Upper = _overlapSettings.OverlapPoint.position - upDirUpper * halfHeightUpper;

        int upperCount = Physics.OverlapCapsuleNonAlloc(
            point1Upper,
            point2Upper,
            _overlapSettings.SphereRadius,
            collidersUpper,
            _overlapSettings.SearchLayer
        );

        for (int q = 0; q < upperCount; q++)
        {
            if (collidersUpper[q] == null)
                continue;

            Debug.Log("Upper Detected");
            Debug.Log("Upper Detected");
            Collider[] collidersDown = new Collider[_overlapDownSettings.Size];

            Physics.OverlapSphereNonAlloc(_overlapDownSettings.OverlapPoint.position, _overlapDownSettings.SphereRadius,
                collidersDown, _overlapDownSettings.SearchLayer);
            for (int i = 0; i < collidersDown.Length; i++)
            {
                if (collidersDown[i] != null)
                {
                    return;
                }
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