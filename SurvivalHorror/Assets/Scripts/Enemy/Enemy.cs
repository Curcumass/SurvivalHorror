using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected Transform _player;
    [SerializeField] protected float _damage;
    [SerializeField] private Transform[] _waypoint;
    [SerializeField] protected float _viewAngle;
    [SerializeField] protected float _detectRange;
    private int _currentPoint = 0;



    protected NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.autoBraking = false;
    }

    protected virtual void DetectPlayer()
    {
        Vector3 target = (_player.position - transform.position).normalized;
        if(Vector3.Angle(transform.forward, target) < _viewAngle)
        {
            float distanceToTarget = Vector3.Distance(transform.position, _player.position);
            if(distanceToTarget < _detectRange)
            {
                _agent.stoppingDistance = 7f;
                Chase();
            }
        }
    }

    protected virtual void Attack()
    {
        
    }

    protected virtual void Chase()
    {
        transform.LookAt(_player);
        _agent.SetDestination(_player.position);
    }

    protected virtual void Patrol()
    {
        if (_waypoint.Length == 0)
        {
            return;
        }
        _agent.destination = _waypoint[_currentPoint].position;
        _currentPoint = (_currentPoint + 1) % _waypoint.Length;
    }
}
