using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] string damageType;
    [SerializeField] int damageAmount = 2;
    [SerializeField] float stunTime = .4f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health targetHP = other.GetComponent<Health>();
        if (targetHP != null)
        {
            targetHP.TakeDamage(damageAmount,stunTime);
        }
    }
}
