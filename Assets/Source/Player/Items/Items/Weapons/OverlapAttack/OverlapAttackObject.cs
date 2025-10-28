using System.Collections;
using UnityEngine;

public class OverlapAttackObject : MonoBehaviour
{
    [SerializeField] private GameObject _hitEffect;

    [SerializeField] private float _chekingRate;

    [SerializeField] private float _length = 2f;
    [SerializeField] private float _radius = 0.5f;
    [SerializeField] private LayerMask _layerMask;
    private RaycastHit[] _others = new RaycastHit[10];

    public void StartCheckingForAttack()
    {
        StopAllCoroutines();
        StartCoroutine(CheckingForAttack());
    }

    private IEnumerator CheckingForAttack()
    {
        while (true)
        {
            _others = new RaycastHit[10];
            Vector3 start = transform.position;
            Vector3 end = transform.position - transform.right * _length;
            //
            // Physics.CapsuleCastNonAlloc(start, end, _radius, -transform.right, _others, _length, _layerMask);


            Collider[] others = new Collider[20];
            Physics.OverlapCapsuleNonAlloc(start, end, _radius, others, _layerMask);

            RaycastHit hit;

            foreach (var i in others)
            {
                if (Physics.Raycast(transform.position, i.transform.position - transform.position, out hit, 100,
                    _layerMask))
                {
                    Instantiate(_hitEffect, hit.point, Quaternion.identity);
                }
            }

            // foreach (var hit in _others)
            // {
            //     Instantiate(_hitEffect, hit.point, Quaternion.identity);
            // }

            yield return new WaitForSeconds(_chekingRate);
        }
    }

    public void StopCheckingForAttack()
    {
        StopAllCoroutines();
    }

    private void OnDrawGizmos()
    {
        Vector3 start = transform.position;
        Vector3 end = transform.position - transform.right * _length;
        Vector3 direction = -transform.right;

        Gizmos.color = Color.cyan;

        DrawCapsule(start, end, _radius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine((start + end) / 2, (start + end) / 2 + direction * _length);
    }

    private void DrawCapsule(Vector3 start, Vector3 end, float radius)
    {
        Vector3 up = (end - start).normalized;
        float height = Vector3.Distance(start, end);
        Quaternion rotation = Quaternion.LookRotation(up);

        Gizmos.DrawWireSphere(start, radius);
        Gizmos.DrawWireSphere(end, radius);

        Vector3[] offsets =
        {
            Vector3.up, Vector3.down, Vector3.left, Vector3.right,
            Vector3.forward, Vector3.back
        };

        foreach (var offset in offsets)
        {
            Gizmos.DrawLine(start + offset * radius, end + offset * radius);
        }
    }
}