using System.Collections;
using UnityEngine;

public class UnitPatrolState : State
{
    [SerializeField] private UnitNavMeshMove _navMeshMove;

    [SerializeField] private PatrolPoint[] _points;

    private Coroutine _patrolingCoroutine;

    public override void Enter()
    {
        StartPatrol();
    }

    public override void Exit()
    {
        StopPatrol();
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
        StopPatrol();
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