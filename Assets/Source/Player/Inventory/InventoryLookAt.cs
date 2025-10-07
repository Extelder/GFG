using System;
using UnityEngine;

public class InventoryLookAt : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, _layerMask))
        {
            transform.LookAt(hit.point, transform.up);
        }
    }
}