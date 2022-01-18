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
    [HideInInspector] public float groundAngle = 0;

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
        Vector2 delta = direction*moveSpeed*(1/(float)accelerationFrames);
        delta.y *= zOffsetRatio;
        Vector2 maxValue = Vector2.one * moveSpeed;
        maxValue.y *= zOffsetRatio;
        maxValue.x = Mathf.Abs(maxValue.x*Mathf.Sign(direction.x)-_rigidBody.velocity.x);
        maxValue.y = Mathf.Abs(maxValue.y*Mathf.Sign(direction.y)-_rigidBody.velocity.y);
        delta.x = Mathf.Clamp(delta.x,-maxValue.x,maxValue.x);
        delta.y = Mathf.Clamp(delta.y,-maxValue.y,maxValue.y);
        _rigidBody.velocity += delta;
        if(Mathf.Sign(_rigidBody.velocity.x) != transform.localScale.x && direction.x != 0)
        {
            FlipSprite();
        }
    }

    public void SlowPlayerX()
    {
        float currentSpeed = _rigidBody.velocity.x;
        Vector2 delta = new Vector2(moveSpeed * Mathf.Sign(currentSpeed)*(1/(float)decelerationFrames),0);
        delta.x = Mathf.Clamp(delta.x,
            (1f - Mathf.Sign(currentSpeed)) / 2 * currentSpeed,
            (1f + Mathf.Sign(currentSpeed)) / 2 * currentSpeed);
        _rigidBody.velocity -= delta;
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

    public IEnumerator SlopeMoveX(float angle)
    {
        do
        {
            Vector2 delta = _rigidBody.velocity;
            delta.x -= Mathf.Sin(angle / Mathf.Rad2Deg) / 5f;
            _rigidBody.velocity = delta;
            if(Mathf.Sign(_rigidBody.velocity.x) != transform.localScale.x && _rigidBody.velocity.x != 0)
            {
                FlipSprite();
            }
            yield return new WaitForFixedUpdate();
        }
        while(groundAngle != 0);
    }
}
