using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer),typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 25;
    [Range(1,0)] [SerializeField] float zOffsetRatio = 0.5f;

    SpriteRenderer _renderer;
    Rigidbody2D _rigidBody;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Move(Vector2 direction)
    {

        Vector2 delta = direction;
        delta.y *= zOffsetRatio;
        delta *= moveSpeed * (Time.fixedDeltaTime * 10f);
        _rigidBody.velocity = delta;
        if( Mathf.Sign(_rigidBody.velocity.x) != transform.localScale.x && _rigidBody.velocity.x != 0)
        {
            FlipSprite();
        }
    }

    void FlipSprite()
    {
            transform.localScale = new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
    }

}
