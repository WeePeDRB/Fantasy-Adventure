using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paladin : CharacterBase
{
    //Character unique stats
    private float resistance = 50f;

    //Character dash
    private float   dashDistance    =   5f;
    private float   dashSpeed       =   13f;
    private float   dashCooldown    =   5f;
    private bool    canDash         =   true;
    private bool    isDashing       =   false;
    private Vector3 dashTarget;
    private bool    specialEffect   =   false;
    private float   specialEffectCooldown = 3f;

    //Event
    public event EventHandler OnWeaponMoveToLeft;
    public event EventHandler OnWeaponMoveToRight;
    
    
    private void Start()
    {
        //Initial stats for character
        InstantiateCharacter();

        //Set a subscriber for the dash action event
        gameInput.OnDashAction += HandleDashSkill;
        gameInput.OnHandleWeaponMovement += HandleWeaponMovement;
    }


    private void Update()
    {
        if (isDashing)
        {
            transform.position = Vector3.MoveTowards(transform.position, dashTarget, dashSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, dashTarget) < 0.1f)
            {
                isDashing = false;
            }

        }
        
        else if (!isDashing)
        {
            HandleMovement();
        }
    }


    private void ResetDash()
    {
        canDash = true;
    }

    private void ResetSkill()
    {
        specialEffect = false;
    }


    protected override void InstantiateCharacter()
    {
        //primaryWeapon = gameObject.AddComponent<Sword>();
        weapons = new List<IWeapon>();
        weapons.Add(primaryWeapon);
        maxWeapon   =   4;
        maxItem     =   3;

        //Initialize stats
        maxHealth   =   150f;
        health      =   150f;
        speed       =   7f;
        maxAmor     =   100f;
        amor        =   100f;
        level       =   1;
    }
    
    protected override void HandleDashSkill(object sender, EventArgs e)
    {
        if(!isDashing && canDash)
        {
            //Set the destination for the dash skill
            dashTarget = transform.position + transform.forward * dashDistance;
            //Set the dashing flag
            isDashing       = true;
            canDash = false;
            //Set the special effect flag
            specialEffect = true;

            //Invoke the cooldown reset for skill and special effect 
            Invoke(nameof(ResetSkill), specialEffectCooldown); //Special effect
            Invoke(nameof(ResetDash), dashCooldown); //Skill cooldown
        }
    }

    protected override void HandleWeaponMovement(object sender, GameInput.HandleWeaponMovementEventArgs e)
    {
        string keyPressed = e.weaponMovementEventArgs;

        switch (keyPressed)
        {
            case "q" :  
                        OnWeaponMoveToLeft?.Invoke(this, EventArgs.Empty);
                        break;
            case "e" :  
                        OnWeaponMoveToRight?.Invoke(this, EventArgs.Empty);
                        break;

        }
    }



    protected override void HandleSpecialSkill(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }
    protected override void HandleUltimateSkill(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }


    //Collider check
    private void OnCollisionEnter(Collision collision)
    {
        if (isDashing && collision.gameObject.CompareTag("Wall"))
        {
            isDashing = false;
        }
    }
}
