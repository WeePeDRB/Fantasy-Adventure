using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    //Character basic stats
    protected string playerName;
    protected float maxHealth;
    protected float health;
    protected float speed;
    protected float maxAmor;
    protected float amor;
    protected int level;


    //Character inventory system
    protected IWeapon primaryWeapon;
    protected List<IWeapon> weapons;
    protected int maxWeapon;
    protected List<IItem> items;
    protected int maxItem;


    //Reference to game input
    [SerializeField] protected GameInput gameInput;


    //Weapon position
    public GameObject forwardPosition;
    public GameObject leftPosition;
    public GameObject rightPosition;
    public GameObject backwardPosition;

    //
    public static CharacterBase Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There are more than one player instance !");
        }
        Instance = this;
    }
    
    /// FUNCTION THAT SHARES ACROSS CLASSES
    

    //
    protected virtual void HandleMovement()
    {
        //Handle Input
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirVector = new Vector3(inputVector.x, 0, inputVector.y);
        //Move
        transform.position += moveDirVector * speed * Time.deltaTime;
        
        //Rotation
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirVector, Time.deltaTime * rotateSpeed);
    }

    //Initial stats and weapon
    protected abstract void InstantiateCharacter();

    //
    protected abstract void HandleWeaponMovement(object sender, GameInput.HandleWeaponMovementEventArgs e);
    protected abstract void HandleDashSkill(object sender, EventArgs e);
    protected abstract void HandleSpecialSkill(object sender, EventArgs e);
    protected abstract void HandleUltimateSkill(object sender, EventArgs e);
}
