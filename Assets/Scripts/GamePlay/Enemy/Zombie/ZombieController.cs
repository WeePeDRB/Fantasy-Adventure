using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonsterBaseController
{
    private void Awake()
    {
       monsterStats = new MonsterStats(100,3,1,0);
    }

    private void Update()
    {
        base.HandleMovement();
    }
}
