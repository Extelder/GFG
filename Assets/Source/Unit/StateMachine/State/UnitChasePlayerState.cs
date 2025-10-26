using System.Collections;
using UnityEngine;

public class UnitChasePlayerState : State
{
    [SerializeField] private UnitNavMeshMove _navMeshMove;
    [SerializeField] private DefaultEnemyAnimator _defaultEnemyAnimator;

    [SerializeField] private float _chaseUpdatePlayerRate;

    public override void Enter()
    {
        CanChanged = false;
        StopAllCoroutines();
        StartCoroutine(Chasing());
    }

    public override void Exit()
    {
        StopAllCoroutines();
    }

    private IEnumerator Chasing()
    {
        while (true)
        {
            _navMeshMove.SetDestination(PlayerCharacter.Instance.PlayerTransform.position);
            _defaultEnemyAnimator.Move();
            yield return new WaitForSeconds(_chaseUpdatePlayerRate);
        }
    }
}