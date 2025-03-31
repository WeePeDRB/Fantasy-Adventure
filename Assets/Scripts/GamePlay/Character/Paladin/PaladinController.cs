using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinController : CharacterBaseController
{


    protected override void HandleDashSkill()
    {
        if (!isDashing && canDash)
        {
            //Set the destination for the dash skill
            dashTarget = transform.position + transform.forward * dashDistance;
            //Set the dashing flag
            isDashing       = true;
            canDash = false;

            //Reset the skill and special effect
            Invoke(nameof(ResetDashSkill), dashCooldown); 
        }
    }
    protected override void HandleSpecialSkill()
    {
    }

    protected override void HandleUltimateSkill()
    {
        throw new NotImplementedException();
    }

    //Collider check
    private void OnCollisionEnter(Collision collision)
    {
        if (isDashing && collision.gameObject.CompareTag("Wall"))
        {
            isDashing = false;
        }
    }



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
