using System;
using UnityEngine;

public class AggresiveUnitStateMachine : UnitMoveStateMachine
{
    [field: SerializeField] protected State ChaseState { get; private set; }
    [field: SerializeField] protected UnitInspectState InspectState { get; private set; }
    [field: SerializeField] protected State AttackState { get; private set; }

    public void Chase()
    {
        ChangeState(ChaseState);
    }

    private void Update()
    {
        Debug.Log(CurrentState);
    }

    public void Inspect(Vector3 inspectPoint)
    {
        InspectState.ChangeInspectPoint(inspectPoint);
        ChangeState(InspectState);
    }

    public void Attack()
    {
        ChangeState(AttackState);
    }
}