using System.Collections;
using UnityEngine;

public class UnitInspectState : State
{
    [SerializeField] private DefaultEnemyAnimator _unitAnimator;
    [SerializeField] private UnitNavMeshMove _move;

    private Vector3 _inspectPoint;

    public void ChangeInspectPoint(Vector3 point)
    {
        if (point == _inspectPoint)
            return;
        _inspectPoint = point;
        Inspect();
    }

    public override void Enter()
    {
        Inspect();
    }

    public void Inspect()
    {
        StopAllCoroutines();
        StartCoroutine(GoToInspect());
    }

    private IEnumerator GoToInspect()
    {
        _unitAnimator.Move();
        _move.SetDestination(_inspectPoint);
        yield return new WaitUntil(() => _move.AgentReachDestination());
        _unitAnimator.Idle();
    }
}