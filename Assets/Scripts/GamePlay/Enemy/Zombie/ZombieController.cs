using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonsterBaseController
{
    //
    // FIELDS
    //

    // EVENTS
    public event Action OnZombieAttack;
    public event Action OnZombieHurt;
    public event Action OnZombieDead;

    //
    // PROPERTIES
    //

    public bool IsMoving { get { return isMoving; } }
    public bool IsDead { get { return isDead; } }

    //
    // FUNCTIONS
    //

    // INITIAL SET UP FOR ZOMBIE
    public override void InstantiateMonster()
    {
        //
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBaseController>();
        
        // Reset the "isDead" bool
        isDead = false;

        // HitBox set up
        if ( monsterBaseHitBox == null) monsterBaseHitBox = GetComponentInChildren<ZombieHitBox>();

        // Listen to HitBox events
        monsterBaseHitBox.OnPlayerEnterMonsterAttackRange += IsReadyToAttack;
        monsterBaseHitBox.OnPlayerExitMonsterAttackRange += IsOutOfRange;

        // Initial stats for zombie
        monsterStats = new MonsterStats(100,2,1,10,2,0,0);

    }

    // HANDLING ZOMBIE BEHAVIOR
    // Zombie movement
    protected override void HandleMovement()
    {
        if ( !isDead && !isPlayerInside && !isAttacking )
        {
            //Specify direction
            Vector3 direction = (player.transform.position - this.transform.position).normalized;
            Vector3 moveDirVector = new Vector3(direction.x, 0, direction.z);

            //Movement
            transform.position += moveDirVector * monsterStats.Speed * Time.deltaTime;

            //Rotation
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirVector, Time.deltaTime * rotateSpeed);

            //
            isMoving = true;
        }
    }

    // Zombie attack
    protected override void IsReadyToAttack()
    {
        //
        isPlayerInside = true;
        isMoving = false;

        // Check if coroutine is start
        if ( attackCoroutine == null )
        {
            isAttacking = true;
            attackCoroutine = StartCoroutine(AttackCoroutine());
        }
    }

    protected override IEnumerator AttackCoroutine()
    {
        while ( !isDead )
        {
            OnZombieAttack?.Invoke();
            yield return new WaitForSeconds(monsterStats.AttackSpeed);
        }
    }

    public override void Attack()
    {
        if (isPlayerInside) player.Hurt(monsterStats.Damage);
    }


    protected override void IsOutOfRange()
    {
        isPlayerInside = false;
        isAttacking = false;
        if ( attackCoroutine != null )
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
    }


    // Zombie get hurt
    public override void Hurt(float damageTaken)
    {
        if ( !isDead )
        {
            monsterStats.Health -= damageTaken;
            
            if ( monsterStats.Health == 0 )
            {
                Dead();
            }
        }
    }

    // Zombie dead
    protected override void Dead()
    {
        OnZombieDead?.Invoke();
        monsterBaseHitBox.OnPlayerEnterMonsterAttackRange -= IsReadyToAttack;
        monsterBaseHitBox.OnPlayerExitMonsterAttackRange -= IsOutOfRange;
        isDead = true;
    }

    // SUPPORT FUNCTION
    
    public override void ReceiveSpecialEffect(SpecialEffectBase specialEffect)
    {
        
    }


    private void Awake()
    {
        InstantiateMonster();
    }

    private void Update()
    {
        HandleMovement();
    }
}
