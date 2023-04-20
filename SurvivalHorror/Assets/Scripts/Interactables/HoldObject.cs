using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldObject : Interactable
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _object;
    [SerializeField] private float _throwForce = 300f;

    [SerializeField] private float _rotationSpeed = 10f;


    private Rigidbody _rigidbody;
    private bool _isHold = false;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = true;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            Drop();
            _isHold = false;
        }

        if (Input.GetMouseButton(0) && _isHold)
        {
            Throw();
            Drop();
            _isHold = false;
        }
        if (_isHold)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _object.transform.parent = _player.transform;
        }
        if (Input.GetMouseButton(1) && _isHold)
        {
            Rotate();
        }
    }

    protected override void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
            _isHold = true;
        }
    }

    private void PickUp()
    {
        //_rigidbody.isKinematic = true;
        _rigidbody.useGravity = false;
        _rigidbody.detectCollisions = true;

    }

    private void Drop()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _object.transform.parent = null;
    }

    private void Throw()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(_player.forward * _throwForce);
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime;

        Vector3 rotation = new Vector3(mouseX * Time.deltaTime * _rotationSpeed, mouseY * Time.deltaTime * _rotationSpeed, 0);

        _object.transform.Rotate(rotation);
    }
}
