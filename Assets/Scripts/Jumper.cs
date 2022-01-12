using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Jumper : MonoBehaviour
{
    [Range(0.01f,2f)][SerializeField] float gravityScale = 0.5f;
    [Range(1,10)][SerializeField] float jumpMaxHeight = 4f;
    [Range(1,50)][SerializeField] float jumpInitialSpeed = 8f;
    [HideInInspector] public float velocity = 0f;
    [HideInInspector] public float slopeVariance = 0f;
    Vector3 initialPosition;
    Vector3 jumpTarget;
    Animator _animator;



    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    public float GetHeight() { return transform.localPosition.y - initialPosition.y; }

    public void Jump()
    {
        if(!_animator.GetBool("isFalling"))
        {
            SetMaxHeight(jumpMaxHeight);
            _animator.SetBool("isAirborn",true);
            velocity = jumpInitialSpeed;
            StartCoroutine(ManageVerticalVelocity());
        }
    }

    public void Fall()
    {
        if(_animator.GetBool("isAirborn"))
        {
            velocity = 0f;
        }
        else
        {
            _animator.SetBool("isAirborn",true);
            velocity = 0f;
            StartCoroutine(ManageVerticalVelocity());
        }
    }

    public void SetMaxHeight(float heightFromGround)
    {
        jumpTarget = initialPosition + (Vector3.up*heightFromGround);
    }

    IEnumerator ManageVerticalVelocity()
    {
        do
        {
            UpdateCollision();
            Vector2 delta = new Vector2();
            delta.y = transform.localPosition.y + (velocity*Time.fixedDeltaTime);
            delta.y = Mathf.Clamp(delta.y,initialPosition.y,jumpTarget.y);
            transform.localPosition = delta;
            velocity = Mathf.Clamp(velocity - (gravityScale * 30f * Time.fixedDeltaTime),-40f,40);
            yield return new WaitForFixedUpdate();
        }
        while ( transform.localPosition != initialPosition );
        UpdateCollision();
        velocity = 0;
        _animator.SetBool("isAirborn",false);
    }

    void UpdateCollision()
    {
        if(GetHeight() > 0f) { SetCollision("Pitfall",true); }
        else{ SetCollision("Pitfall",false); }
    }

    void SetCollision(string layer,bool ignored)
    {
        Physics2D.IgnoreLayerCollision (transform.parent.gameObject.layer, LayerMask.NameToLayer(layer), ignored);
    }
}
