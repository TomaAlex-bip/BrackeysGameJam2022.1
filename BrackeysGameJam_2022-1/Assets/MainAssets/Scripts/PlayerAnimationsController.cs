using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationsController : MonoBehaviour
{

    public static PlayerAnimationsController Instance { get; private set; }

    private Animator animator;

    private const string IDLE_ANIMATION = "IdleAnimation";
    private const string WALK_ANIMATION = "WalkAnimation";
    private const string RUN_ANIMATION = "RunAnimation";
    private const string FALL_ANIMATION = "FallAnimation";


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        
        PlayIdleAnimation();
    }





    public void PlayIdleAnimation() => animator.Play(IDLE_ANIMATION);
    
    
    public void PlayWalkAnimation() => animator.Play(WALK_ANIMATION);
    
    public void PlayRunAnimation() => animator.Play(RUN_ANIMATION);

    public void PlayFallAnimation() => animator.Play(FALL_ANIMATION);

}
