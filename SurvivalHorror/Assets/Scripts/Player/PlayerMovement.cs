using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Header("Movement properties")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _movementSpeed = 7f;    
    [SerializeField] private float _crouchSpeed = 3f;
    [SerializeField] private float _airControl = 1f;
    [SerializeField] private float _jumpHeight = 1f;

    [Header("Ground and gravity properties")]
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;

    [Header("Crouch properties")]
    [SerializeField] private float _normalHeight;
    [SerializeField] private float _crouchHeight;

    [SerializeField] private Health _playerHealth;


    private Vector3 _velocity;
    private bool _isGrounded;
    private bool _wasGrounded;
    private float _fallTime;

    private Vector3 lastDirection;
    public Vector3 moveDirection; // for HeadBob script

    private bool _isCrouch;


    private void FixedUpdate()
    {
        GroundCheck();

        MovePlayer();

        GravityControl();

        CalculateFallingTime();

        if (!_wasGrounded && _isGrounded)
        {
            _playerHealth.TakeDamage(_fallTime * 100);
        }

        _wasGrounded = _isGrounded;
    }
    private void Update()
    {
        Crouch();

        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            Jump();
        }
    }

    private void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        moveDirection = move;

        if (_isGrounded)
        {
            lastDirection = moveDirection;
        }
        if (!_isGrounded)
        {
            moveDirection = lastDirection + (moveDirection * _airControl);
        }
        //_characterController.Move(moveDirection * _movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _movementSpeed = 3.5f;
            _crouchSpeed = 2f;
        }
        else
        {
            _movementSpeed = 7f;
            _crouchSpeed = 3f;
        }
        if (_isCrouch)
        {
            _characterController.Move(moveDirection * _crouchSpeed * Time.deltaTime);
        }
        else
        {
            _characterController.Move(moveDirection * _movementSpeed * Time.deltaTime);
        }
    }

    private void GravityControl()
    {
        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);

        if (_isGrounded && _velocity.y < 0 && _wasGrounded)
        {
            _velocity.y = -2f;
        }
    }

    private void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
    }

    private void Jump()
    {

        _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);

    }

    private void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            _characterController.height = _crouchHeight;
            _isCrouch = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            _characterController.height = _normalHeight;
            _isCrouch = false;
        }
    }

    private void CalculateFallingTime()
    {
        if (_velocity.y < -7)
        {
            _fallTime += Time.deltaTime;
        }
        else
        {
            _fallTime = 0;
        }
    }

}
