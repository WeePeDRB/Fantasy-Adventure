using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinController : CharacterBaseController
{
    //  Character unique stats
    private float resistance = 50f; 


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


    public void listenToenemy()
    {
        Debug.Log("player is listen !");
    }


    //
    //
    //
    private void Start()
    {
        //Initial stats for character
        //InstantiateCharacter(150f, 7f, 100f, 1);
        
        //Initial for dash skill
        //InstantiateDash(5f, 13f, 5f, 3f);

        //Set a subscriber for the dash action event
        GameInput.OnDashAction += HandleDashSkill;
    }


    private void Update()
    {
        HandleMovement();
    }

}
