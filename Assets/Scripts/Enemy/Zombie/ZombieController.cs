using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonsterBaseController
{
    private void Awake()
    {
        base.InstantiateCharacter(100,2,2);
    }

    private void Update()
    {
        base.HandleMovement();
    }
}
