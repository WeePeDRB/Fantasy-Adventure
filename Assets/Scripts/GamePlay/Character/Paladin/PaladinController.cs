using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinController : CharacterBaseController
{


    protected override void HandleSpecialSkill(object sender, EventArgs e)
    {
    }
    protected override void HandleUltimateSkill(object sender, EventArgs e)
    {
    }


    //Collider check
    private void OnCollisionEnter(Collision collision)
    {
        if (isDashing && collision.gameObject.CompareTag("Wall"))
        {
            isDashing = false;
        }
    }

    //
    //
    //
    private void Start()
    {
        //Set a subscriber for the dash action event
        GameInput.OnDashAction += HandleDashSkill;
    }


    private void Update()
    {
        HandleMovement();
    }

}
