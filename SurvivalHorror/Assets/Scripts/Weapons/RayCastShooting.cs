using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastShooting : MonoBehaviour
{
    [SerializeField] private float _shootDistance = 11f;
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private int _damage;
    [SerializeField] private int _criticalDamage;

    [SerializeField] private float _fireRate;
    [SerializeField] private int _clipSize;

    [SerializeField] private AudioSource _shootSound;

    private bool _canShoot;

    private int _currentAmmoInClip;

    private void Start()
    {
        _canShoot = true;
        _currentAmmoInClip = _clipSize;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _currentAmmoInClip > 0 && _canShoot)
        {
            _canShoot = false;
            StartCoroutine(Shoot());
            _currentAmmoInClip--;
        }

        Debug.DrawRay(_playerCamera.transform.position, _playerCamera.transform.forward * _shootDistance, Color.red);
    }

    private IEnumerator Shoot()
    {
        _shootSound.pitch = Random.Range(0.8f, 1.2f);
        _shootSound.Play();
        Ray ray = new Ray(_playerCamera.transform.position, _playerCamera.transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, _shootDistance))
        {
            Debug.Log(hit.transform.name);

            if(hit.transform.TryGetComponent<EnemyHealth>(out EnemyHealth health))
            {
                if(hit.collider is BoxCollider)
                {
                    health.TakeDamage(_criticalDamage);
                }
                else
                {
                    health.TakeDamage(_damage);
                }
            }
        }

        yield return new WaitForSeconds(_fireRate);
        _canShoot = true;
    }
}
