using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController :  MonoBehaviour, IMonsterController
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

    //
    // FUNCTIONS
    //

    // INITIAL SET UP FOR ZOMBIE
    public void InstantiateMonster()
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
        zombieStats.InitialMonsterStats(10,100,3,2,0,1);
    }

    // MONSTER BEHAVIOR
    // Monster movement
    public void HandleMovement()
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
    public void IsReadyToAttack()
    {
        //
        isPlayerInside = true;
        isMoving = false;

        // Check if coroutine is start
        if ( attackCoroutine == null )
        {
            attackCoroutine = StartCoroutine(AttackCoroutine());
        }
    }

    public IEnumerator AttackCoroutine()
    {
        while ( isDead == false)
        {
            Attack();
            yield return new WaitForSeconds(zombieStats.AttackSpeed);
        }
    }

    public void Attack()
    {
        OnZombieAttack?.Invoke();
        if (isPlayerInside) CharacterBaseController.Instance.Hurt(zombieStats.Damage);
    }


    public void IsOutOfRange()
    {
        isPlayerInside = false;
        isAttacking = false;
        if ( attackCoroutine != null )
        {
            StopCoroutine(AttackCoroutine());
            attackCoroutine = null;
        }
    }


    // Monster get hurt
    public void Hurt(float damageTaken)
    {
        if (isDead == false)
        {
            //OnMonsterHurt?.Invoke();
            zombieStats.Health -= damageTaken;
            Debug.Log("This is monster health: "+ zombieStats.Health);
            if ( zombieStats.Health == 0 )
            {
                Debug.Log("Dead function from hurt");
                Dead();
            }
        }
    }

    // Monster dead
    public void Dead()
    {
        OnZombieDead?.Invoke();
        ZombieBaseHitBox.OnPlayerEnterMonsterAttackRange -= IsReadyToAttack;
        ZombieBaseHitBox.OnPlayerExitMonsterAttackRange -= IsOutOfRange;
        isDead = true;
    }

    // SUPPORT FUNCTION
    public void ReceiveSpecialEffect(SpecialEffectBase specialEffect)
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
