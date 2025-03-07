using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    //
    //  Character basic stats
    //
    protected string playerName;
    protected float maxHealth;
    protected float health;
    protected float speed;
    protected float maxAmor;
    protected float amor;
    protected int level;

    //
    //  Character inventory system
    //
    protected IWeapon primaryWeapon;
    protected List<IWeapon> weapons;
    protected int maxWeapon;
    protected List<IItem> items;
    protected int maxItem;


    //
    //  Summary:
    //      Positions around player which will allow the weapon to move around,
    //      each weapon will have special effect 
    //
    public GameObject forwardPosition;
    public GameObject leftPosition;
    public GameObject rightPosition;
    public GameObject backwardPosition;

    //
    //  Player instance
    //
    public static CharacterBase Instance { get; private set; }


    //
    //
    //
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There are more than one player instance !");
        }
        Instance = this;
    }
    


    //
    //  Summary:
    //      Take the player input and move the character
    //
    protected virtual void HandleMovement()
    {
        //Handle Input
        Vector2 inputVector = GameInput.GetMovementVectorNormalized();
        Vector3 moveDirVector = new Vector3(inputVector.x, 0, inputVector.y);
        //Move
        transform.position += moveDirVector * speed * Time.deltaTime;
        
        //Rotation
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirVector, Time.deltaTime * rotateSpeed);
    }

    //
    //  Summary:
    //      Take place when player get hit
    //
    protected virtual void Hurt()
    {
        Debug.Log("Player get hit !");
    }

    //
    //  Summary: 
    //      Initial stats and base weapon for character
    //
    protected abstract void InstantiateCharacter();


    //
    //  These functions used to handle the GameInput event
    //


    //
    //  Summary:
    //      Control the weapon movement
    //
    //  Parameters:
    //      sender: 
    //      e: the information that passed down from the event
    protected abstract void HandleWeaponMovement(object sender, GameInput.HandleWeaponMovementEventArgs e);
  

    //
    //  Summary:
    //      Handle the dash skill 
    //
    //  Parameters:
    //      sender: 
    //      e: 
    protected abstract void HandleDashSkill(object sender, EventArgs e);


    //
    //  Summary:
    //      Handle the special skill
    //
    //  Parameters:
    //      sender: 
    //      e: 
    protected abstract void HandleSpecialSkill(object sender, EventArgs e);


    //
    //  Summary:
    //      Handle the ultimate skill
    //
    //  Parameters:
    //      sender: 
    //      e: 
    protected abstract void HandleUltimateSkill(object sender, EventArgs e);
}
