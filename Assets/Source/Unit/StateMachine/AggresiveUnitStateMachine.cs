using UnityEngine;

public class AggresiveUnitStateMachine : UnitMoveStateMachine
{
    [field: SerializeField] protected State ChaseState { get; private set; }
    [field: SerializeField] protected State InspectChase { get; private set; }
    [field: SerializeField] protected State AttackState { get; private set; }

    public void Chase()
    {
        ChangeState(ChaseState);
    }

    public void Inspect()
    {
        ChangeState(InspectChase);
    }

    public void Attack()
    {
        ChangeState(AttackState);
    }
}