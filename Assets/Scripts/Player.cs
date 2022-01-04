using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Mover),typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    Vector2 rawInput;
    Rigidbody2D _rigidBody;
    Jumper _jumper;
    Animator _animator;
    
    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _jumper = GetComponentInChildren<Jumper>();
        _animator = _jumper.GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        _animator.SetFloat("MoveX",rawInput.x);
        _animator.SetFloat("MoveY",rawInput.y);
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && !_animator.GetBool("pressedJump")) StartCoroutine(_jumper.ManageAnimatorBools());
    }
}
