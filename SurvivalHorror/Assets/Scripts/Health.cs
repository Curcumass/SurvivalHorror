using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        if( _currentHealth <= 0)
        {
            Death();
        }
        _audioSource.Play();
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
