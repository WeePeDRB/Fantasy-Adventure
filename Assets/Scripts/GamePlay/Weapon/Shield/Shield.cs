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

    // EQUIP WEAPON
    public void EquipWeapon()
    {

    }
    
    // UPGRADE WEAPON (Increase weapon stats)  
    public void UpgradeWeapon()
    {
        weaponLevel ++;
        weaponAttackDamage += weaponLevel * 5;

    }
    
    // WEAPON ATTACK LOGIC
    // Deal damage to monster
    public void Attack()
    {
        for (int i =0; i < monsterListInHitBox.Count; i++)
        {
            monsterListInHitBox[i].Hurt(weaponAttackDamage);
        }
    }
    // Attack coroutine
    public IEnumerator AttackCoroutine()
    {
        while (player.HeroStats.Health > 0)
        {
            Attack();
            yield return new WaitForSeconds(weaponAttackSpeed);
        }
    }

    // SUPPORT FUNCTIONS
    // Trigger detect
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            MonsterBaseController monsterBaseController = collider.gameObject.GetComponent<MonsterBaseController>();
            
            // Add monster to hit box list
            monsterListInHitBox.Add(monsterBaseController);

            // Subscribe to monster dead event (Remove monster from list incase monster die)
            monsterBaseController.OnMonsterDead += CheckIfMonsterDead;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            MonsterBaseController monsterBaseController = collider.gameObject.GetComponent<MonsterBaseController>();

            // Remove monster from hit box list
            monsterListInHitBox.Remove(monsterBaseController);

            // Unsubscribe to monster dead event
            monsterBaseController.OnMonsterDead -= CheckIfMonsterDead;
        }
    }

    // Check monster list
    private void CheckIfMonsterDead(object sender, MonsterBaseController.OnMonsterDeadEventArgs monsterDeadEventArgs)
    {
        for (int i = 0; i < monsterListInHitBox.Count; i ++)
        {
            if (monsterListInHitBox[i] == monsterDeadEventArgs.monsterBaseController)
            {
                monsterListInHitBox.Remove(monsterListInHitBox[i]);
            }
        }
    }

    private void Start()
    {
        monsterListInHitBox = new List<MonsterBaseController>();
        player = GetComponentInParent<HeroBaseController>();
        StartCoroutine(AttackCoroutine());
    }

}
