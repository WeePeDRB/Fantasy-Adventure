using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IWeapon
{
    //
    //  Weapon colliders
    //
    [SerializeField] private GameObject weaponHitBox;   //  The hit box collider
    [SerializeField] private GameObject weaponParryBox; //  The parry hit box collider

    //
    //  References
    //
    [SerializeField] private Paladin player;    //  Player reference

    //
    //  Weapon stats
    //
    private float   weaponAttackSpeed;  
    private float   weaponAttackDamage;
    private int     weaponLevel;

    //
    //  Flag for the moving function
    //
    public bool weaponMove;



    private void Start()
    {
        transform.SetParent(player.backwardPosition.transform);
        transform.localPosition = Vector3.zero;


        //
        //  Set up for the event
        //
        player.OnWeaponMoveToLeft += HandleMoveToLeft;
        player.OnWeaponMoveToRight += HandleMoveToRight;
    }

    
    private void Update()
    {
        Attack();

        if (weaponMove)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, Time.deltaTime * 5f);
            
            if (transform.localPosition == Vector3.zero)
            {
                weaponMove = false;
            }
        }
    }

    //
    //  
    //
    public void Attack()
    {

    }
    public void EquipWeapon()
    {

    }
    public void UpgradeWeapon()
    {
        
    }


    public void HandleMoveToLeft(object sender, EventArgs e)
    {
        MoveToLeft(CheckParent());
    }

    public void HandleMoveToRight(object sender, EventArgs e)
    {
        MoveToRight(CheckParent());
    }

    public void MoveToLeft(int parentPos)
    {
        
        if (parentPos == 1)
        {
            SetParent(player.leftPosition.transform);
        }
    
        if (parentPos == 2)
        {
            SetParent(player.backwardPosition.transform);
        }

        if (parentPos == 3)
        {
            SetParent(player.rightPosition.transform);
        }

        if (parentPos == 4)
        {
            SetParent(player.forwardPosition.transform);
        }
    }

    public void MoveToRight(int parentPos)
    {
        if (parentPos == 1)
        {
            SetParent(player.rightPosition.transform);
        }
    
        if (parentPos == 2)
        {
            SetParent(player.forwardPosition.transform);
        }

        if (parentPos == 3)
        {
            SetParent(player.leftPosition.transform);
        }

        if (parentPos == 4)
        {
            SetParent(player.backwardPosition.transform);
        }
    }


    //Check the current weapon's parent
    public int CheckParent()
    {
        if (transform.parent == null)
        {
            Debug.LogError("Weapon is not on parent");
        }

        //Forward
        if (transform.parent.name == "Forward")
        {
            return 1;
        }
        //Left
        else if (transform.parent.name == "Left")
        {
            return 2;
        }
        //Backward
        else if (transform.parent.name == "Backward")
        {
            return 3;
        }
        //Right
        else if (transform.parent.name == "Right")
        {
            return 4;
        }
        return 0;
    }

    //Set the parent object for weapon
    public void SetParent(Transform nextParentTransform)
    {
        transform.SetParent(nextParentTransform.transform);
        weaponMove = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            
        }
    }
}
