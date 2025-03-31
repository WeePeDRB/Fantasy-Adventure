using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseAnimator : MonoBehaviour
{   
    //
    // FIELDS
    //
    
    // ANIMATOR 
    // References
    private Animator animator;
    // Animator parameters
    private const string IS_MOVING = "IsMoving"; // Parameter name
    private bool isMoving; // Parameter value



    //
    // FUNCTIONS
    //

    // HANDLING CHARACTER ANIMATION
    // Control player movement animation
    private void MovementAnimation()
    {   
        Vector2 inputVector = GameInput.GetMovementVectorNormalized();
        if(inputVector != Vector2.zero) isMoving = true;
        else isMoving = false;
        animator.SetBool(IS_MOVING, isMoving);
    }

    // Control dash animation
    private void DashAnimation()
    {
        animator.SetTrigger("Dash");
    }


    //
    //
    //
    private void Awake()
    {
        animator = GetComponent<Animator>();
        GameInput.OnDashAction += DashAnimation;
    }

    private void Update()
    {
        MovementAnimation();
    }

}
