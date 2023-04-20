using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage = 15;
    private float _timeToDestroy = 0;

    private void Update()
    {
        _timeToDestroy += Time.deltaTime;
        if(_timeToDestroy > 3)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<Health>(out Health health))
        {
            health.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}
