using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] string damageType;
    [SerializeField] int damageAmount;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit!" + other);
    }
}
