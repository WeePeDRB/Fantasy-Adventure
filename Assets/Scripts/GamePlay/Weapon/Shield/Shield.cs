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
    [SerializeField] private PaladinController player;    //  Player reference

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



    }

    
    private void Update()
    {
        Attack();
    }

    //
    //  
    //
    public void EquipWeapon()
    {

    }
    
    
    public void UpgradeWeapon()
    {
        
    }
    
    
    public void Attack()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            
        }
    }
}
