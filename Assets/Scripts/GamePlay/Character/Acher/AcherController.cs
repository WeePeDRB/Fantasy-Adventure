using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcherController : CharacterBaseController
{

    protected override void HandleDashSkill()
    {
    }
    protected override void HandleSpecialSkill()
    {
    }
    protected override void HandleUltimateSkill()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }
}
