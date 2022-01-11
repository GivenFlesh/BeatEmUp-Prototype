using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer),typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;
    [Range(1,0)] [SerializeField] float zOffsetRatio = 0.5f;
    [Range(1,500)] public int accelerationFrames = 10;
    [Range(1,500)] public int decelerationFrames = 10;
    float defaultMoveSpeed;
    int defaultAccelFrames;
    int defaultDecelFrames;

    SpriteRenderer _renderer;
    Rigidbody2D _rigidBody;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        defaultMoveSpeed = moveSpeed;
        defaultAccelFrames = accelerationFrames;
        defaultDecelFrames = decelerationFrames;
    }

    public void MoveFixedSpeed(Vector2 direction)
    {
        Vector2 delta = direction;
        delta.y *= zOffsetRatio;
        delta *= moveSpeed * (Time.fixedDeltaTime * 10f);
        _rigidBody.velocity = delta;
    }

    void FlipSprite()
    {
            transform.localScale = new Vector3(transform.localScale.x*-1,transform.localScale.y,transform.localScale.z);
    }

    public void MoveWithAcceleration(Vector2 direction)
    {
        Vector2 delta = _rigidBody.velocity;
        if(Mathf.Abs(delta.x) < moveSpeed || Mathf.Sign(direction.x) != Mathf.Sign(delta.x))
        {
            delta.x += (direction.x*moveSpeed*(1/(float)accelerationFrames));
            delta.x = Mathf.Clamp(delta.x,-moveSpeed,moveSpeed);
        }
        if(Mathf.Abs(delta.y) < moveSpeed*zOffsetRatio || Mathf.Sign(direction.y) != Mathf.Sign(delta.y))
        {
            delta.y += (direction.y*(moveSpeed*zOffsetRatio)*(1/(float)accelerationFrames));
            delta.y = Mathf.Clamp(delta.y,-moveSpeed*zOffsetRatio,moveSpeed*zOffsetRatio);
        }
        _rigidBody.velocity = delta;
        if(Mathf.Sign(_rigidBody.velocity.x) != transform.localScale.x && direction.x != 0)
        {
            FlipSprite();
        }
    }   

    public void SlowPlayerX()
    {
        float delta = _rigidBody.velocity.x;
        if (delta > 0f)
        {
            delta -= moveSpeed*(1/(float)decelerationFrames);
            delta = Mathf.Clamp(delta,Mathf.Epsilon,moveSpeed);
        }
        if (delta < 0f)
        {
            delta += moveSpeed*(1/(float)decelerationFrames);
            delta = Mathf.Clamp(delta,-moveSpeed,Mathf.Epsilon);
        }
        _rigidBody.velocity = new Vector2(delta,_rigidBody.velocity.y);
    }

    public void SlowPlayerY()
    {
        float delta = _rigidBody.velocity.y;
        if (delta > 0f)
        {
            delta -= moveSpeed*zOffsetRatio*(1/(float)decelerationFrames);
            delta = Mathf.Clamp(delta,0f,moveSpeed*zOffsetRatio);
        }
        if (delta < 0f)
        {
            delta += moveSpeed*zOffsetRatio*(1/(float)decelerationFrames);
            delta = Mathf.Clamp(delta,-moveSpeed*zOffsetRatio,0f);
        }
        _rigidBody.velocity = new Vector2(_rigidBody.velocity.x,delta);
    }

}
