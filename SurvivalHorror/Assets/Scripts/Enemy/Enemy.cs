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

    protected NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
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

    }
}
