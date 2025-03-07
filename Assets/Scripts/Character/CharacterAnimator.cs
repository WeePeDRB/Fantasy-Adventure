using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;

    //
    //  Reference
    //
    [SerializeField] private GameInput gameInput;   //  Reference to game actions
    [SerializeField] private CharacterBase character;   //  Reference to player

    //
    //  Animator parameters
    //
    
    //  Movement
    private const string VELOCITY = "Velocity";  //  Parameter name
    private float velocityFloat;  //  Paramter value


    //
    //
    //
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MovementAnimation();
    }


    //
    //  Summary:
    //      Control the player movement animation
    //
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


}
