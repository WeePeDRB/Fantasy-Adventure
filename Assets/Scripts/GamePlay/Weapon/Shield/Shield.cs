using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IWeapon
{
    //
    // FIELDS
    //

    // Weapon colliders
    private SphereCollider weaponHitBox;   //  The hit box collider

    //  References
    private CharacterBaseController player;    //  Player reference

    //  Weapon stats
    private float   weaponAttackSpeed;  
    private float   weaponAttackDamage;
    private int     weaponLevel;

    //
    private List<GameObject> monsterListInHitBox;



    //
    // FUNCTIONS
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

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            monsterListInHitBox.Add(collider.gameObject);
        }
    }

    private void Start()
    {
        player = CharacterBaseController.Instance;
    }

    
    private void Update()
    {
       
    }
}
