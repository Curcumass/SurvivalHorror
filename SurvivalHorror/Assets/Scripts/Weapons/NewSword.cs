using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSword : MonoBehaviour
{
    [Header("Attack parameters")]
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private float _damage = 10f;

    [Header("Charge parameters")]
    [SerializeField] private float _chargeSpeed = 1f;
    [SerializeField] private float _chargeTime;

    [Header("Camera parameters")]
    [SerializeField] private Camera _camera;
    [SerializeField] private float _normalFov = 75f;
    [SerializeField] private float _chargeFov = 90f;
    [SerializeField] private float _fovChangeTime = 2f;


    private bool _canAttack = true;
    private bool _isAttacking = false;
    private bool _isCharging = false;

    private void Update()
    {
        if (Input.GetMouseButton(0) && _canAttack && _chargeTime < 2)
        {
            _damage = 10f;

            _chargeTime += Time.deltaTime * _chargeSpeed;
            Debug.Log(_chargeTime.ToString());

            if (_chargeTime > 0.3f)
            {              
                ChargeAttack();
                _isCharging = true;
            }
        }

        if (Input.GetMouseButtonUp(0) && _canAttack)
        {
            if (_chargeTime >= 1f)
            {
                _damage = 20f;
            }
            if (_isCharging)
            {
                ReleaseChargeAttack();                
            }
            else
            {
                NormalAttack();
            }
        }
    }

    private void NormalAttack()
    {
        _isAttacking = true;
        _canAttack = false;

        _isCharging = false;
        _chargeTime = 0;

        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("NormalAttack");

        StartCoroutine(ResetAttackCooldown());
    }

    private void ChargeAttack()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("isCharging", true);

        _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _chargeFov, _fovChangeTime * Time.deltaTime);

        Debug.Log("CHARGE");
    }
    private void ReleaseChargeAttack()
    {
        _isAttacking = true;
        _canAttack = false;

        _isCharging = false;
        _chargeTime = 0;


        Animator animator = GetComponent<Animator>();
        animator.SetBool("isCharging", false);

        StartCoroutine(ResetAttackCooldown());

        Debug.Log("RELEASE ATTACK");
        _camera.fieldOfView = _normalFov;
    }

    private IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(_attackCooldown);
        _canAttack = true;
    }

    private IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(0.2f);
        _isAttacking = false;
    }

    // ¬от ту нужно подумать получше, так как когда оружие изначально во враге - урон не считаетс€
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<EnemyHealth>(out EnemyHealth health) && _isAttacking)
        {
            health.TakeDamage(_damage);
        }
    }
}
