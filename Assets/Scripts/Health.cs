using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 12;
    [SerializeField] int currentHealth;
    bool canBeHit = true;
    Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public bool Hittable()
    {
        return canBeHit;
    }

    public void TakeDamage(int damage, float stunTime)
    {
        currentHealth -= damage;
        if(currentHealth <= 0) { Die(); }
        else
        {
            if(_animator != null)
            {
                _animator.SetBool("isHit",true);
                _animator.SetFloat("stunTime",stunTime);
            }
        }

    }

    void Die()
    {
        if (transform.parent != null)
        Destroy(transform.parent.gameObject);
        else Destroy(gameObject);
    }

}
