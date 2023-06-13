using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    public PlayerAnimation(Animator newAnimator)
    {
        animator = newAnimator;
    }
    public void Idle()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Walk", false);
    }
    public void Walk()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Walk", true);
    }
    public void Run()
    {
        animator.SetBool("Run", true);
    }
}
