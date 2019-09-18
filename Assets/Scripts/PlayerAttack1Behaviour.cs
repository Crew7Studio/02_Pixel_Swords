using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack1Behaviour : StateMachineBehaviour {

    PlayerController player;


    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}




    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = FindObjectOfType<PlayerController>();
        animator.SetBool("Attack1", false);
            // Obviously "noOfClicks" is not directly accessible here so you have to make it public and get it reference somehow to use it here 
         if (player.noOfClicks >= 2)
        {
            // animator.SetBool("Attack2", true);
            animator.SetTrigger("attack2");
          //  Debug.Log("Attack 2222222222222222222222222");
        }
    }
    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}


