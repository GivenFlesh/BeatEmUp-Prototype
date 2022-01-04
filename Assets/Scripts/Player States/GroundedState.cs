using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : StateMachineBehaviour
{
    Mover _mover;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _mover = animator.GetComponentInParent<Mover>();
        animator.SetFloat("jumpMomentumX",0);
        animator.SetFloat("jumpMomentumY",0);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _mover.Move(new Vector2(animator.GetFloat("MoveX"),animator.GetFloat("MoveY")));
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       _mover.Move(Vector2.zero);
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
