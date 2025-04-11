using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonsterBaseController
{
    //
    // FIELDS
    //

    // MONSTER STATS
    public MonsterStats zombieStats;

    // MONSTER EFFECT STATUS
    protected EffectStatus effectStatus;

    // CHECKING FLAGS
    protected bool isMoving;
    protected bool isPlayerInside;
    protected bool isAttacking;
    protected bool isDead;
    public bool checking; 

    // EVENTS
    public event Action OnZombieAttack;
    public event Action OnZombieHurt;
    public event Action OnZombieDead;

    // REFERENCE 
    protected MonsterBaseHitBox ZombieBaseHitBox;

    // COROUTINE VALUE
    protected Coroutine attackCoroutine;

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
        // Reset the "isDead" bool
        isDead = false;

        // HitBox set up
        if ( ZombieBaseHitBox == null) ZombieBaseHitBox = GetComponentInChildren<ZombieHitBox>();

        // Listen to HitBox events
        ZombieBaseHitBox.OnPlayerEnterMonsterAttackRange += IsReadyToAttack;
        ZombieBaseHitBox.OnPlayerExitMonsterAttackRange += IsOutOfRange;

        // Initial stats for zombie
        zombieStats = new MonsterStats();
        zombieStats.InitialMonsterStats(10,100,3,2.16f,0,1);
    }

    // MONSTER BEHAVIOR
    // Monster movement
    protected override void HandleMovement()
    {
        if ( isDead == false && isPlayerInside == false && isAttacking == false )
        {
            //Specify direction
            Vector3 direction = (CharacterBaseController.Instance.transform.position - this.transform.position).normalized;
            Vector3 moveDirVector = new Vector3(direction.x, 0, direction.z);

            //Movement
            transform.position += moveDirVector * zombieStats.Speed * Time.deltaTime;

            //Rotation
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirVector, Time.deltaTime * rotateSpeed);

            //
            isMoving = true;
        }
    }

    // Monster attack
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
        while ( isDead == false)
        {
            OnZombieAttack?.Invoke();
            yield return new WaitForSeconds(zombieStats.AttackSpeed);
        }
    }

    public override void Attack()
    {
        if (isPlayerInside) CharacterBaseController.Instance.Hurt(zombieStats.Damage);
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


    // Monster get hurt
    public override void Hurt(float damageTaken)
    {
        if (isDead == false)
        {
            //OnMonsterHurt?.Invoke();
            zombieStats.Health -= damageTaken;
            
            if ( zombieStats.Health == 0 )
            {
                Dead();
            }
        }
    }

    // Monster dead
    protected override void Dead()
    {
        OnZombieDead?.Invoke();
        ZombieBaseHitBox.OnPlayerEnterMonsterAttackRange -= IsReadyToAttack;
        ZombieBaseHitBox.OnPlayerExitMonsterAttackRange -= IsOutOfRange;
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
        if (attackCoroutine != null) checking = true;
        else checking = false;
    }
}
