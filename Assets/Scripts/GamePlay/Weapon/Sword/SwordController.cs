using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : WeaponBase
{
    //
    // FIELDS
    //
    
    // List contain monsters that get hit
    private List<MonsterBaseController> monsterListInHitBox;

    // Events
    public event Action OnWeaponAttack;

    //
    // FUNCTIONS
    // 

    //
    public override void WeaponPowerUp()
    {
        weaponAttackDamage += 10f;
    }

    // WEAPON ATTACK LOGIC
    // Deal damage to monster
    public void ApplyDamage()
    {
        float bonusDamage = 0f;
        if (heroBaseController.HeroStats.DamageAmplifier != 0)
        {
            bonusDamage = weaponAttackDamage * heroBaseController.HeroStats.DamageAmplifier / 100;
        }
        for (int i = 0; i < monsterListInHitBox.Count; i++)
        {
            monsterListInHitBox[i].Hurt(weaponAttackDamage + bonusDamage);
        }
    }
    // Attack coroutine
    public override IEnumerator AttackCoroutine()
    {
        while (heroBaseController.HeroStats.Health > 0)
        {
            yield return new WaitForSeconds(weaponAttackSpeed);
            OnWeaponAttack?.Invoke();
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
    private void CheckIfMonsterDead(object sender, OnMonsterDeadEventArgs monsterDeadEventArgs)
    {
        monsterDeadEventArgs.monsterBaseController.OnMonsterDead -= CheckIfMonsterDead;
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
        heroBaseController = GetComponentInParent<HeroBaseController>();
        StartCoroutine(AttackCoroutine());
        Debug.Log("Weapon start");
    }

}
