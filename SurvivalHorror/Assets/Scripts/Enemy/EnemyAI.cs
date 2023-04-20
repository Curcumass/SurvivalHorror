using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private NavMeshAgent _agent;

    [SerializeField] private float _viewRadius;
    [SerializeField] private float _viewAngle;

    [SerializeField] private LayerMask _target;
    [SerializeField] private LayerMask _obstacle;

    [SerializeField] Transform[] _waypoints;
    private int _currentPoint = 0;

    private bool _findPlayer = false;
    private bool _patrol = true;
    

    private void Start()
    {        
        _agent = GetComponent<NavMeshAgent>();
        _agent.autoBraking = false;
    }
    private void Update()
    {
        DetectPlayer();

        if (_findPlayer)
        {
            _patrol = false;
            Chase();

        }

        if (!_agent.pathPending && _agent.remainingDistance < 0.5f && _patrol)
        {
            Patrol();
        }
    }

    private void Chase()
    {
        _agent.SetDestination(_player.transform.position);
    }

    private void DetectPlayer()
    {
        Vector3 target = (_player.transform.position - transform.position).normalized;
        if (Vector3.Angle(transform.forward, target) < _viewAngle)
        {
            float distanceToTarget = Vector3.Distance(transform.position, _player.transform.position);
            if (distanceToTarget <= _viewRadius)
            {
                if (Physics.Raycast(transform.position, target, distanceToTarget, _obstacle) == false)
                {
                    _findPlayer = true;
                }
            }
        }
    }

    private void Patrol()
    {
        if(_waypoints.Length == 0)
        {
            return;
        }
        _agent.destination = _waypoints[_currentPoint].position;
        _currentPoint = (_currentPoint +1) % _waypoints.Length;

    }
}
