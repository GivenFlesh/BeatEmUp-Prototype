using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer),typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 25;
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

    public void MoveWithMomentum(Vector2 direction)
    {
        float delta = _rigidBody.velocity.x;
        delta += (direction.x*moveSpeed*(1/(float)accelerationFrames));
        _rigidBody.velocity = new Vector2(Mathf.Clamp(delta,-moveSpeed,moveSpeed),_rigidBody.velocity.y);
    }   

    public void SlowPlayer()
    {
        float delta = _rigidBody.velocity.x;
        if (delta > 0f)
        {
            delta -= (moveSpeed*(1/(float)decelerationFrames))*Time.fixedDeltaTime;
            delta = Mathf.Clamp(delta,Mathf.Epsilon,moveSpeed*Time.fixedDeltaTime);
        }
        if (delta < 0f)
        {
            delta += (moveSpeed*(1/(float)decelerationFrames))*Time.fixedDeltaTime;
            delta = Mathf.Clamp(delta,-moveSpeed*Time.fixedDeltaTime,Mathf.Epsilon);
        }
        _rigidBody.velocity = new Vector2(delta,_rigidBody.velocity.y);
    }


}
