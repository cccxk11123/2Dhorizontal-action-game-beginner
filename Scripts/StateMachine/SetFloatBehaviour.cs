using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFloatBehaviour : StateMachineBehaviour
{
    public string setBoolName;
    public bool onStateEnter, onStateExit;
    public bool onStateMachineEnter, onStateMachineExit;
    public float setEnterValue, setExitValue;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(onStateEnter)
            animator.SetFloat(setBoolName, setEnterValue);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(onStateExit)
            animator.SetFloat(setBoolName, setExitValue);
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
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if(onStateMachineEnter)
            animator.SetFloat(setBoolName, setEnterValue);
    }
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if(onStateMachineExit)
            animator.SetFloat(setBoolName, setExitValue);
    }
}
