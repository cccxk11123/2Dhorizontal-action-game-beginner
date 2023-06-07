using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolBehaviour : StateMachineBehaviour
{
    public string setBoolName;
    public bool updateState;
    public bool updateStateMachine;
    public bool setEnterValue, setExitValue;
    
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(updateState)
        {
            animator.SetBool(setBoolName, setEnterValue);
        }
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(updateState)
        {
            animator.SetBool(setBoolName, setExitValue);
        }
    }

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        if(updateStateMachine)
        {
            animator.SetBool(setBoolName, setEnterValue);
        }
    }

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        if(updateStateMachine)
        {
            animator.SetBool(setBoolName, setExitValue);
        }
    }
}
