using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour, IWeapon
{
    //
    // FIELDS
    //

    // Weapon data
    [SerializeField] private SO_Weapon weaponData;

    // Reference
    private HeroBaseController heroBaseController;

    // Weapon stats
    private float weaponAttackSpeed;
    private float weaponAttackDamage;
    private int weaponLevel;
    
    // List contain monsters that get hit
    private List<MonsterBaseController> monsterListInHitBox;

    // Events
    public event Action OnWeaponAttack;

    //
    // FUNCTIONS
    // 

    // INITIALIZE STATS FOR WEAPON
    public void InitializeWeapon()
    {
        weaponAttackSpeed = weaponData.weaponAttackSpeed;
        weaponAttackDamage = weaponData.weaponDamage;
        weaponLevel = weaponData.weaponLevel;
    }

    // EQUIP WEAPON
    public void EquipWeapon()
    {

    }

    // UPGRADE WEAPON (Increase weapon stats)  
    public void UpgradeWeapon()
    {
        
    }

    // WEAPON ATTACK LOGIC
    // Deal damage to monster
    public void ApplyDamage()
    {
        for (int i =0; i < monsterListInHitBox.Count; i++)
        {
            monsterListInHitBox[i].Hurt(weaponAttackDamage);
        }
    }
    // Attack coroutine
    public IEnumerator AttackCoroutine()
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
    private void CheckIfMonsterDead(object sender, MonsterBaseController.OnMonsterDeadEventArgs monsterDeadEventArgs)
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
        InitializeWeapon();
        StartCoroutine(AttackCoroutine());
    }
}
