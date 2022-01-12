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
        velocity = jumpInitialSpeed;
        _animator.SetBool("isAirborn",true);
        jumpTarget = new Vector3 (initialPosition.x,initialPosition.y+jumpMaxHeight);
        StartCoroutine(ManageVerticalVelocity());
    }

    public void Fall()
    {
        velocity = -0.5f;
        _animator.SetBool("isAirborn",true);
        _animator.SetBool("isFalling",true);
        jumpTarget = transform.position+Vector3.up;
        StartCoroutine(ManageVerticalVelocity());
    }

    IEnumerator ManageVerticalVelocity()
    {
        do
        {
            CheckCollision();
            Vector2 delta = new Vector2();
            delta.y = transform.localPosition.y + (velocity*Time.fixedDeltaTime);
            delta.y = Mathf.Clamp(delta.y,initialPosition.y,jumpTarget.y);
            transform.localPosition = delta;
            velocity = Mathf.Clamp(velocity - (gravityScale * 30f * Time.fixedDeltaTime),-40f,40);
            yield return new WaitForFixedUpdate();
        }
        while ( transform.localPosition != initialPosition );
        CheckCollision();
        velocity = 0;
        _animator.SetBool("isAirborn",false);
        _animator.SetBool("isFalling",false);
    }

    void CheckCollision()
    {
        if(GetHeight() > 0f) { SetCollision("Pitfall",true); }
        else{ SetCollision("Pitfall",false); }
    }

    void SetCollision(string layer,bool ignored)
    {
        Physics2D.IgnoreLayerCollision (gameObject.layer, LayerMask.NameToLayer(layer), ignored);
    }
}
