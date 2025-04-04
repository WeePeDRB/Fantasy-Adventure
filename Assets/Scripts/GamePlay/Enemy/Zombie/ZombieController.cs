using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonsterBaseController
{
    private void Awake()
    {
       
    }

    private void Update()
    {
        base.HandleMovement();
    }
}
