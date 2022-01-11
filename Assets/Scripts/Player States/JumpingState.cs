using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : StateMachineBehaviour
{
    Mover _mover;
    Vector2 jumpMomentum;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       _mover = animator.GetComponentInParent<Mover>();
       jumpMomentum = new Vector2(animator.GetFloat("jumpMomentumX"),animator.GetFloat("jumpMomentumY"));
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         Vector2 delta = new Vector2 (animator.GetFloat("MoveX"),animator.GetFloat("MoveY"));
         delta.x += (Mathf.Sign(jumpMomentum.x)*2);   delta.x = Mathf.Abs(delta.x/3f);
         delta.y += (Mathf.Sign(jumpMomentum.y)*2);   delta.y = Mathf.Abs(delta.y/3f);
         _mover.MoveFixedSpeed(jumpMomentum*delta);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
