using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySenses : MonoBehaviour
{
    [Header("View properties")]
    [SerializeField] private float _viewRadius;
    [SerializeField] private float _viewAngle;

    [SerializeField] private LayerMask _target;
    [SerializeField] private LayerMask _obstacle;

    [SerializeField] private GameObject _player;

    bool _findPlayer = false;

    private void Update()
    {
        Vector3 target = (_player.transform.position - transform.position).normalized;
        if(Vector3.Angle(transform.forward, target) < _viewAngle / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, _player.transform.position);
            if(distanceToTarget <= _viewRadius)
            {
                if(Physics.Raycast(transform.position, target, distanceToTarget, _obstacle) == false)
                {
                    _findPlayer = true;
                }
            }
        }
    }
}
