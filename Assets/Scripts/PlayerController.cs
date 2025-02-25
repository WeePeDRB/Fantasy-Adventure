using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    //Player movement set up
        //Physics set up
        private Vector2 moveDirInput;
        private Vector3 moveDir;
        private float inputLength;
        private bool isWalking;
        private bool isRunning;
        public float speed;

        

        //Take input from player
        public void OnMove(InputAction.CallbackContext context){
            moveDirInput = context.ReadValue<Vector2>();
            moveDir = new Vector3(moveDirInput.x,0f,moveDirInput.y);
        }

        //Move player
        public void MovePlayer(){
            Debug.Log("This is the moveDir value" + moveDir);


            //Rotate
            if(moveDirInput != Vector2.zero){
                transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(moveDir),0.15f);
            }
            
            //Move character
            transform.Translate(moveDir * velocityFloat * speed * Time.deltaTime, Space.World);
        }

        //Animator set up
        private Animator playerAnimator;
        private float velocityFloat;
        private int paramHash;

        public float accelerateMultiply;
        public float decelerateMultiply;

        //
        public void GetInput(){
            if (moveDirInput != Vector2.zero){

                inputLength = Mathf.Sqrt(Mathf.Pow(moveDirInput.x,2) + Mathf.Pow(moveDirInput.y,2));

                if (inputLength < 0.7f){
                    isWalking = true;
                    isRunning = false;
                }else {
                    isRunning = true;
                    isWalking = false;
                }
            }else{
                isWalking = false;
                isRunning = false;
            }
        }

        public void Animate(){
            velocityFloat = inputLength;
            if (velocityFloat > 1f){
                    velocityFloat = 1f;
                }
            if (!isWalking && !isRunning && velocityFloat > 0){
                velocityFloat = 0;
            }


            playerAnimator.SetFloat(paramHash,velocityFloat);
        }


    

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        paramHash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        GetInput();
        Animate();
    }
}
