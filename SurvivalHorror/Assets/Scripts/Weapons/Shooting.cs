using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed = 500f;
    [SerializeField] private float _upwardForce = 0f;
    [SerializeField] private Camera _camera;

    [SerializeField] private float _fireRate;
    [SerializeField] private int _clipSize;
    [SerializeField] private int _reserveAmmo;

    private bool _canShoot;
    private int _currentAmmoInClip;
    private int _ammoInReserve;

    [SerializeField] private Vector3 _normalLocalPosition;
    [SerializeField] private Vector3 _aimingLocalPosition;
    [SerializeField] private float aimSmoothing = 10f;

    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        _currentAmmoInClip = _clipSize;
        _ammoInReserve = _reserveAmmo;
        _canShoot = true;
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && _canShoot && _currentAmmoInClip > 0)
        {
            _canShoot = false;
            _currentAmmoInClip -= 1;
            StartCoroutine(Shoot());
        }
        else if(Input.GetKeyDown(KeyCode.R) && _currentAmmoInClip < _clipSize && _ammoInReserve > 0)
        {
            int amountNeeded = _clipSize - _currentAmmoInClip;
            if(amountNeeded > _ammoInReserve)
            {
                _currentAmmoInClip += _ammoInReserve;
                _ammoInReserve -= amountNeeded;
            }
            else
            {
                _currentAmmoInClip = _clipSize;
                _ammoInReserve -= amountNeeded;
            }
        }

        Aim();

    }

    private void Aim()
    {
        Vector3 target = _normalLocalPosition;
        if (Input.GetMouseButton(1))
        {
            target = _aimingLocalPosition;
        }

        Vector3 desirePosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);
        transform.localPosition = desirePosition;
    }

    private IEnumerator Shoot()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // луч по центру экрана.
        RaycastHit raycastHit;

        Vector3 targetPoint;
        if(Physics.Raycast(ray, out raycastHit))
        {
            targetPoint = raycastHit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(750);
        }

        GameObject bullet = Instantiate(_bulletPrefab, _shootPoint.position, Quaternion.identity);

        Vector3 direction = targetPoint -  _shootPoint.position;
        bullet.transform.forward = direction.normalized;

        _audioSource.Play();

        bullet.GetComponent<Rigidbody>().AddForce(direction.normalized * _bulletSpeed, ForceMode.Impulse);
        bullet.GetComponent<Rigidbody>().AddForce(_camera.transform.up * _upwardForce, ForceMode.Impulse);

        
        yield return new WaitForSeconds(_fireRate);
        _canShoot = true;     
    }

}
