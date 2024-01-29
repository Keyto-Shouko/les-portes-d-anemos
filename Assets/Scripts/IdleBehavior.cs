using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{
    public float timer = 5f;
    private int randomInt;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       randomInt = Random.Range(0, 2);
       //re set the timer value
        timer = 5f;
        animator.ResetTrigger("LifeSentence");
        animator.ResetTrigger("Judgement");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer -= Time.deltaTime;
        if(timer <= 0){
            if(randomInt == 0){
                animator.SetTrigger("LifeSentence");
            }
            else{
                animator.SetTrigger("Judgement");
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
