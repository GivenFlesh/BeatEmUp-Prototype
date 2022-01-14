using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Transform targetTransform;
    Mover _mover;

    void Awake()
    {
        targetTransform = FindObjectOfType<Player>().transform;
        _mover = GetComponent<Mover>();
    }

    void Update()
    {
        Vector2 target = targetTransform.position;
        target.x += Mathf.Sign(transform.position.x-target.x);
        target.y += Mathf.Sign(transform.position.y-target.y)/2;

        // _mover.MoveFixedSpeed(target);
    }

}
