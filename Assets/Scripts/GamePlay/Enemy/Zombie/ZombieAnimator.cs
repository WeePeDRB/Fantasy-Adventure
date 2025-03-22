using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimator : MonsterBaseAnimator
{
    private void Awake()
    {
        base.InstantiateAnimator();
    }

    private void Update()
    {
        base.Moving();
    }
}
