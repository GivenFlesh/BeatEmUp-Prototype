using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

[RequireComponent(typeof(Mover),typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    Vector2 rawInput;
    Rigidbody2D _rigidBody;
    Jumper _jumper;
    Animator _animator;
    public TextMeshProUGUI debugText;

    
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
        debugText.text = _rigidBody.velocity.ToString();
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        string animatorBool = "pressedJump";
        if (value.isPressed && !_animator.GetBool(animatorBool))
        {
            StartCoroutine(ManageAnimatorBools(animatorBool));
        }
    }

    void OnAttackLight(InputValue value)
    {
        string animatorBool = "pressedAttack";
        if (value.isPressed && !_animator.GetBool(animatorBool))
        {
            StartCoroutine(ManageAnimatorBools(animatorBool));
        }
    }


    IEnumerator ManageAnimatorBools(string animatorBool)
    {
        _animator.SetBool(animatorBool,true);
        yield return new WaitForFixedUpdate();
        _animator.SetBool(animatorBool,false);
    }
}
