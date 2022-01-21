using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStartState : StateMachineBehaviour
{
    Jumper _jumper;
    Rigidbody2D _rigidbody;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      _jumper = animator.GetComponent<Jumper>();
      _rigidbody = animator.GetComponentInParent<Rigidbody2D>();
      animator.SetFloat("jumpMomentumX",_rigidbody.velocity.x - ( _jumper.jumpPower * Mathf.Sin(_jumper.slopeAngle / Mathf.Rad2Deg)));
      animator.SetFloat("jumpMomentumY",_rigidbody.velocity.y);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      float delta = _rigidbody.velocity.x * Mathf.Tan(_jumper.slopeAngle / Mathf.Rad2Deg) / 1.5f;
      _jumper.Jump(delta);
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
