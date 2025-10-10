using System;
using UnityEngine;

public class InventoryLookAt : MonoBehaviour
{
    [SerializeField] private GameObject _targetObject;

    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField, Range(0.1f, 20f)] private float rotationSpeed = 5f;
    [SerializeField] private bool ignoreY = false;

    [SerializeField] private Vector3 _lookOffset = new Vector3(0f, 1f, 0f);

    private Vector3 targetPoint = new Vector3(0, 0, 0);


    private void LateUpdate()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);


        RaycastHit[] hits = new RaycastHit[10];
        hits = Physics.RaycastAll(ray, 10f, _layerMask);
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider == null)
                continue;

            if (hits[i].collider.gameObject == _targetObject)
            {
                targetPoint = hits[i].point;
                if (ignoreY)
                    targetPoint.y = transform.position.y;
            }
        }

        Vector3 direction = (targetPoint - transform.position + _lookOffset).normalized;
        if (direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, transform.up);
            transform.rotation =
                Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}