using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimator : MonoBehaviour
{
    //Animator
    private Animator animator;

    //
    [SerializeField] private Zombie zombie;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

    }


    private void EnemyReady()
    {
        zombie.EnemyReady();
    }
}
