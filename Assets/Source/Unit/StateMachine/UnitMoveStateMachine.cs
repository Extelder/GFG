using UnityEngine;

public class UnitMoveStateMachine : StateMachine
{
    [field: SerializeField] protected State MoveState { get; private set; }

    public void Move()
    {
        ChangeState(MoveState);
    }
}