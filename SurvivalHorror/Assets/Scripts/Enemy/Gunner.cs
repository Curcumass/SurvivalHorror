using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunner : Enemy
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _bulletSpeed = 15f;

    [SerializeField] private float _shootCooldown = 1;
    [SerializeField] private float _shootingDistance = 7f;
    private bool _canShoot = true;


    private void Update()
    {
        if(!_agent.pathPending && _agent.remainingDistance < 0.5f)
        {
            Patrol();
            _agent.stoppingDistance = 0;
        }

        DetectPlayer();

        if (Vector3.Distance(_agent.transform.position, _player.transform.position) <= _agent.stoppingDistance && _canShoot)
        {
            Attack();
        }
    }

    protected override void Attack()
    {
        _canShoot = false;
        GameObject bullet = Instantiate(_bullet, _shootPoint.position, Quaternion.identity);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = transform.forward * _bulletSpeed;

        StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(_shootCooldown);
        _canShoot = true;
    }
}
