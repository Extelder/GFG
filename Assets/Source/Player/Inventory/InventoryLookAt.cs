using System;
using UnityEngine;

public class InventoryLookAt : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField, Range(0.1f, 20f)] private float rotationSpeed = 5f;
    [SerializeField] private bool ignoreY = false;

    [SerializeField] private Vector3 _lookOffset = new Vector3(0f, 1f, 0f);

    private void LateUpdate()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
        {
            Vector3 targetPoint = hit.point + _lookOffset;
            if (ignoreY)
                targetPoint.y = transform.position.y;

            Vector3 direction = (targetPoint - transform.position).normalized;

            if (direction.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, transform.up);
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}