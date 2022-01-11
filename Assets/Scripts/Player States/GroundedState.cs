using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : StateMachineBehaviour
{
    Mover _mover;
    Rigidbody2D _rigidbody;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _mover = animator.GetComponentInParent<Mover>();
        _rigidbody = animator.GetComponentInParent<Rigidbody2D>();
        animator.SetFloat("jumpMomentumX",0);
        animator.SetFloat("jumpMomentumY",0);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 dpad = new Vector2 (animator.GetFloat("MoveX"),animator.GetFloat("MoveY"));
        _mover.MoveWithAcceleration(dpad);
        if (dpad.x == 0 && !_mover.onSlope || Mathf.Sign(_rigidbody.velocity.x) != Mathf.Sign(dpad.x))
        { _mover.SlowPlayerX(); }
        if (dpad.y == 0 || Mathf.Sign(_rigidbody.velocity.y) != Mathf.Sign(dpad.y))
        { _mover.SlowPlayerY(); }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       _mover.MoveWithAcceleration(Vector2.zero);
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
