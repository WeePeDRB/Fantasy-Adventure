using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBaseAnimator : MonoBehaviour
{
    private Animator animator;

    // Reference
    [SerializeField] private CharacterBaseController character; // Reference to player

    // Animator parameters
    private const string VELOCITY = "Velocity"; // Parameter name
    private float velocityFloat;  //  Paramter value


    // Control the player movement animation
    private void MovementAnimation()
    {   
        //Handle Input
        Vector2 inputVector = GameInput.GetMovementVectorNormalized();

        //
        float inputLength;
        if(inputVector != Vector2.zero)
        {
            inputLength = Mathf.Sqrt(Mathf.Pow(inputVector.x,2) + Mathf.Pow(inputVector.y,2));
            
            velocityFloat = inputLength;
            if(velocityFloat > 1)
            {
                velocityFloat = 1f;
            }
        }
        else
        {
            velocityFloat = 0;
        }
        animator.SetFloat(VELOCITY, velocityFloat);
    }


    //
    private void DashAnimation(object sender, EventArgs e)
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
