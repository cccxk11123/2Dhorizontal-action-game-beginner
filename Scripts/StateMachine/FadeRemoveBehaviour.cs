using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeRemoveBehaviour : StateMachineBehaviour
{
    public float fadeTime = 1f;
    public float destroyTime = 2f;
    private float timeElapsed = 0f;
    private float fadeTimer = 0f;
    private SpriteRenderer sr;
    private GameObject obj;
    private Color color;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed = 0f;
        sr = animator.GetComponent<SpriteRenderer>();
        color = sr.color;
        obj = animator.gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timeElapsed += Time.deltaTime;
        if(timeElapsed >= destroyTime)
        {
            fadeTimer += Time.deltaTime;
            float newAphla = sr.color.a * (1 - (fadeTimer / fadeTime));
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, newAphla);
        }
         
        if(timeElapsed >= fadeTime + destroyTime)
        {
            Destroy(obj);
        }
    }
}
