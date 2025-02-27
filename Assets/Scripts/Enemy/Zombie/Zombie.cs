using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : EnemyBase
{

    // Start is called before the first frame update
    void Start()
    {
        InstantiateCharacter();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReady)
        {
            HandleMovement();
        }
    }

    protected override void InstantiateCharacter()
    {
        maxHealth = 100f;
        health = maxHealth;
        speed = 1.3f;
        isReady = false;
    }
}
