using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;
    bool canBeHit = true;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public bool Hittable()
    {
        return canBeHit;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0) { Die(); }
    }

    void Die()
    {
        Destroy(GetComponentInParent<GameObject>());
    }

}
