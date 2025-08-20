using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController :  WeaponBaseOld
{
    //
    // FIELDS
    //

    // List contain monsters that get hit
    private List<MonsterBaseController> monsterListInHitBox;
    private MonsterBaseController closestMonster;

    [SerializeField] private Transform spawnPosition;
    //
    // FUNCTIONS
    //

    public override void WeaponPowerUp()
    {
        weaponAttackDamage += 10f;
    }

    // WEAPON ATTACK LOGIC
    // Deal damage to monster
    public void ApplyDamage(MonsterBaseController monsterBaseController)
    {
        float bonusDamage = 0f;
        if (heroBaseController.HeroStats.DamageAmplifier != 0)
        {
            bonusDamage = weaponAttackDamage * heroBaseController.HeroStats.DamageAmplifier / 100;
        }
        monsterBaseController.Hurt(weaponAttackDamage + bonusDamage);
    }
    // Attack coroutine
    public override IEnumerator AttackCoroutine()
    {
        while (heroBaseController.HeroStats.Health > 0)
        {
            yield return new WaitForSeconds(weaponAttackSpeed);
            if (closestMonster != null) FireProjectile();
        }
    }
    // Get data from arrow
    public void GetDataFromArrow(object sender, ArrowProjectile.OnProjectileHitEventArgs onProjectileHitEventArgs)
    {
        ApplyDamage(onProjectileHitEventArgs.monsterBaseController);
    }
    // Fire projectile
    public void FireProjectile()
    {
        // Get projectile from pool
        GameObject projectileObject = ArrowObjectPool.Instance.GetObject(spawnPosition);
        ArrowProjectile arrowProjectile = projectileObject.GetComponent<ArrowProjectile>();

        //
        arrowProjectile.InitializeProjectile(10, closestMonster.transform.position, 4);

        // Subscribe to event for data return
        arrowProjectile.OnProjectileHit += GetDataFromArrow;
        arrowProjectile.OnProjectileReturn += ReturnArrow;
    }
    public void ReturnArrow(object sender, ArrowProjectile.OnProjectileHitEventArgs onProjectileReturnEventArgs)
    {
        ArrowProjectile arrowProjectile = onProjectileReturnEventArgs.arrowProjectile;
        arrowProjectile.OnProjectileHit -= GetDataFromArrow;
        arrowProjectile.OnProjectileReturn -= ReturnArrow;
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

    // Find closest monster
    public MonsterBaseController FindClosestMonster(List<MonsterBaseController> monsterList)
    {
        if (monsterList == null || monsterList.Count == 0)
        {
            return null;
        }

        MonsterBaseController closestMonster = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (var monster in monsterList)
        {
            if (monster == null) continue;

            float distance = Vector3.SqrMagnitude(monster.gameObject.transform.position - currentPosition);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestMonster = monster;
            }
        }
        return closestMonster;
    }

    private void Start()
    {
        monsterListInHitBox = new List<MonsterBaseController>();
        heroBaseController = GetComponentInParent<HeroBaseController>();
        StartCoroutine(AttackCoroutine());
                Debug.Log("Weapon start");
    }

    private void Update()
    {
        closestMonster = FindClosestMonster(monsterListInHitBox);
    }
}
