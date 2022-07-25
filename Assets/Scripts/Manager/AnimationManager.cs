using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager
{


    public void ChangeAnimationState(Animator animator, string newState, string currentState)
    {
        if (currentState == newState) return;

        animator.Play(newState);

        currentState = newState;
    }






}
