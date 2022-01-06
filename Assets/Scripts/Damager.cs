using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] string damageType;
    [SerializeField] int damageAmount = 2;
    [SerializeField] float stunTime = .4f;

    Collider2D punchTarget;

    void OnTriggerEnter2D(Collider2D other)
    {
        Health targetHP = other.GetComponent<Health>();
        if (targetHP != null)
        {
            targetHP.TakeDamage(damageAmount,stunTime);
            punchTarget = other;
        }
    }

    void OnEnable()
    {
        if (punchTarget != null)
        {
            punchTarget.GetComponent<Health>().TakeDamage(damageAmount,stunTime);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(punchTarget == other)
        {
            punchTarget = null;
        }    
    }
}
