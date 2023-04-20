using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackCooldown;
    private bool _canAttack = true;
    private bool _isAttacking = false;

    private float _chargeCounter;

    [SerializeField] private Vector3 _normalLocalPosition;
    [SerializeField] private Vector3 _attackLocalPosition;
    [SerializeField] private float _chargeSmoothing = 100f;

    private Vector3 _targetPosition;


    private void Start()
    {
        transform.localPosition = _normalLocalPosition;
    }

    private void Update()
    {
        ChargeAttack();
    }

    private void ChargeAttack()
    {
        if (Input.GetMouseButtonDown(0) && _canAttack)
        {
            _targetPosition = _attackLocalPosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            _targetPosition = _normalLocalPosition;

            _isAttacking = true;
            _canAttack = false;
            StartCoroutine(ResetAttackCooldown());
        }

        Vector3 desirePosition = Vector3.Lerp(transform.localPosition, _targetPosition, Time.deltaTime * _chargeSmoothing);
        transform.localPosition = desirePosition;
    }

    private void NormalAttack()
    {
        _isAttacking = true;
        _canAttack = false;

        StartCoroutine(ResetAttackCooldown());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Health>(out Health health) && _isAttacking)
        {
            health.TakeDamage(_damage);
        }
    }

    private IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }
    private IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        _isAttacking = false;
    }
}
