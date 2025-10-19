using System;
using System.Collections;
using UnityEngine;

public class UnitNavMeshPatrol : MonoBehaviour
{
    [SerializeField] private UnitNavMeshMove _navMeshMove;

    [SerializeField] private PatrolPoint[] _points;

    private Coroutine _patrolingCoroutine;

    private void Start()
    {
        StartPatrol();
    }

    public void StartPatrol()
    {
        if (_patrolingCoroutine != null)
            StopCoroutine(_patrolingCoroutine);
        _patrolingCoroutine = StartCoroutine(Patroling());
    }

    public void StopPatrol()
    {
        if (_patrolingCoroutine != null)
            StopCoroutine(_patrolingCoroutine);
    }

    private void OnDisable()
    {
        if (_patrolingCoroutine != null)
            StopCoroutine(_patrolingCoroutine);
    }

    private IEnumerator Patroling()
    {
        int index = -1;
        while (true)
        {
            index++;
            if (index > _points.Length - 1)
            {
                index = 0;
            }

            Vector3 point = _points[index].transform.position;
            _navMeshMove.MoveToAnPoint(point, 0);
            yield return new WaitUntil(() => _navMeshMove.AgentReachDestination() == true);
            yield return new WaitForSeconds(_points[index].TimeOnPoint);
        }
    }
}