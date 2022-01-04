using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStartState : StateMachineBehaviour
{
    Vector2 jumpAngle;
    Jumper _jumper;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      _jumper = animator.GetComponent<Jumper>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      jumpAngle = new Vector2(animator.GetFloat("MoveX"),animator.GetFloat("MoveY"));
      animator.SetFloat("jumpMomentumX",jumpAngle.x);
      animator.SetFloat("jumpMomentumY",jumpAngle.y);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      _jumper.Jump();
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
