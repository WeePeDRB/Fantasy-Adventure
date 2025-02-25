using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private GameInput gameInput;
    [SerializeField] private CharacterBase character;

    //Animator parameters
    //Movement
    private const string VELOCITY = "Velocity";
    private float velocityFloat;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MovementAnimation();
    }

    private void MovementAnimation()
    {   
        //Handle Input
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

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
