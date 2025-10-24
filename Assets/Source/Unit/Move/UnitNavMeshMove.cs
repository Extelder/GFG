using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitNavMeshMove : MonoBehaviour
{
    [SerializeField] private DefaultEnemyAnimator _animator;

    private float _currentPointPriority;
    [SerializeField] private NavMeshAgent _agent;


    public void MoveToAnPoint(Vector3 point, float priority)
    {
        StopAllCoroutines();

        if (_currentPointPriority < priority)
        {
            SetDestination(point);
            return;
        }

        if (_agent.remainingDistance <= 1f)
        {
            SetDestination(point);
        }
    }

    public void SetDestination(Vector3 point)
    {
        _agent.SetDestination(point);
        _animator.Move();
        StartCoroutine(WaitingForEnd());
    }

    private IEnumerator WaitingForEnd()
    {
        yield return new WaitUntil(() => AgentReachDestination() == true);
        _animator.Idle();
    }

    public bool AgentReachDestination() => _agent.remainingDistance <= 0.1f;
}