using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimator : MonoBehaviour
{
    //
    //  Animator
    //
    private Animator animator;  // Animator reference

    //
    //  Reference to the original game object
    //
    [SerializeField] private Zombie zombie; //  Zombie control reference


    //
    //
    //
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }


    //
    //  Summary:
    //      This function will run in the end of the stand up animation 
    //
    private void EnemyReady()
    {
        zombie.EnemyReady();
    }
}
