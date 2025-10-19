using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class UnitNavMeshMove : MonoBehaviour
{
    private float _currentPointPriority;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public void MoveToAnPoint(Vector3 point, float priority)
    {
        if (_currentPointPriority < priority)
        {
            _agent.SetDestination(point);
            return;
        }

        if (_agent.remainingDistance <= 1f)
        {
            _agent.SetDestination(point);
        }
    }

    public bool AgentReachDestination() => _agent.remainingDistance <= 0.5f;
}