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

    // References
    private HeroBaseController player;    //  Player reference

    // Weapon stats
    [SerializeField] private float   weaponAttackSpeed;  
    [SerializeField] private float   weaponAttackDamage;
    [SerializeField] private int     weaponLevel;

    // List contain monsters that get hit
    private List<MonsterBaseController> monsterListInHitBox;

    //
    // FUNCTIONS
    //

    //
    public void EquipWeapon()
    {

    }
    
    
    public void UpgradeWeapon()
    {
        weaponLevel ++;
        weaponAttackDamage += weaponLevel * 5;

    }
    
    public void Attack()
    {
        foreach (MonsterBaseController monster in monsterListInHitBox)
        {
            monster.Hurt(weaponAttackDamage);
        }
    }

    public IEnumerator AttackCoroutine()
    {
        while (player.HeroStats.Health > 0)
        {
            Attack();
            yield return new WaitForSeconds(weaponAttackSpeed);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            monsterListInHitBox.Add(collider.gameObject.GetComponent<MonsterBaseController>());
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            monsterListInHitBox.Remove(collider.gameObject.GetComponent<MonsterBaseController>());
        }
    }

    private void Start()
    {
        monsterListInHitBox = new List<MonsterBaseController>();
        player = GetComponentInParent<HeroBaseController>();
        StartCoroutine(AttackCoroutine());
    }

}
