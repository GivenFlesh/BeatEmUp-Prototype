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
    [SerializeField] Transform shadowObject;
    [HideInInspector] public float velocity = 0f;
    [HideInInspector] public float slopeAngle = 0f;
    [HideInInspector] public Vector3 groundPosition;
    [HideInInspector] public bool isOnSlope;
    Vector3 jumpTarget;
    Animator _animator;
    [HideInInspector] public bool isAirborn = false;
    Vector2 shadowDefaultPosition;


    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        groundPosition = transform.localPosition;
        shadowDefaultPosition = shadowObject.localPosition;
    }

    void Update()
    {
        if(!isAirborn)
        {
            transform.localPosition = groundPosition;
        }
        shadowObject.localPosition = new Vector2 (0f, shadowDefaultPosition.y - GetHeight());
    }

    public float GetHeight() { return transform.localPosition.y - groundPosition.y; }

    public void Jump(float strength)
    {
        if(!_animator.GetBool("isFalling"))
        {
            SetMaxHeight(jumpMaxHeight);
            _animator.SetBool("isAirborn",true);
            velocity = jumpInitialSpeed*strength;
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
        jumpTarget = groundPosition + (Vector3.up*heightFromGround);
    }

    IEnumerator ManageVerticalVelocity()
    {
        isAirborn = true; 
        do
        {
            UpdateCollision();
            Vector2 delta = new Vector2();
            delta.y = transform.localPosition.y + (velocity*Time.deltaTime);
            delta.y = Mathf.Clamp(delta.y,groundPosition.y,float.MaxValue); 
            transform.localPosition = delta;
            velocity = Mathf.Clamp(velocity - (gravityScale * 30f * Time.deltaTime),-40f,40);
            yield return new WaitForEndOfFrame();
        }
        while ( transform.localPosition != groundPosition );
        UpdateCollision();
        velocity = 0;
        _animator.SetBool("isAirborn",false);
        isAirborn = false;
    }

    void UpdateCollision()
    {
        if(GetHeight() > 0f) { SetCollision("Pitfall",true); }
        else{ SetCollision("Pitfall",false); }
        if(GetHeight() > 0.75f) { SetCollision("Character",true); }
        else{ SetCollision("Character",false); }
        if(GetHeight() > 0.75f) { SetCollision("Obstacle",true); }
        else{ SetCollision("Obstacle",false); }
    }

    void SetCollision(string layer,bool ignored)
    {
        Physics2D.IgnoreLayerCollision (transform.parent.gameObject.layer, LayerMask.NameToLayer(layer), ignored);
    }
}
