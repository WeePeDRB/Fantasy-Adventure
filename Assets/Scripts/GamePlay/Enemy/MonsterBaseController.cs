using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public abstract class MonsterBaseController : MonoBehaviour
{ 
    //
    // FIELDS
    //

    // MONSTER DATA
    [SerializeField] protected SO_Monster monsterData;

    // MONSTER BEHAVIOR STATE
    protected MonsterBehavior monsterBehaviorState;

    // CHECKING FLAGS
    protected bool isPlayerInside;

    // MONSTER STATS
    protected MonsterStats monsterStats;

    // MONSTER EFFECT STATUS
    protected MonsterEffectStatus effectStatus;

    // COROUTINE VALUE
    protected Coroutine readyAttackCoroutine;
    protected Coroutine attackCoroutine;

    // MONSTER BEHAVIOR EVENTS
    public event Action OnMonsterAttack;
    public event Action OnMonsterHurt;
    public event EventHandler<OnMonsterDeadEventArgs> OnMonsterDead;

    

    // MONSTER PHYSICS 
    protected Rigidbody monsterRigidbody;
    protected CapsuleCollider monsterCollider;

    // REFERENCE 
    protected MonsterBaseHitBox monsterBaseHitBox;
    protected HeroBaseController player;
    
    // Custom class for event args
    public class OnMonsterDeadEventArgs : EventArgs
    {
        public MonsterBaseController monsterBaseController;
    }

    //
    // PROPERTIES
    //
    public SO_Monster MonsterData
    {
        get { return monsterData; }
    }
    public MonsterStats MonsterStats
    {
        get { return monsterStats; }
    }
    public MonsterBehavior MonsterBeHaviorState 
    { 
        get { return monsterBehaviorState; } 
    }

    // INITIAL SET UP FOR MONSTER
    // First set up for monster
    public virtual void InstantiateMonster()
    {
        // Set variables
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HeroBaseController>();
        monsterCollider = GetComponent<CapsuleCollider>();
        monsterRigidbody = GetComponent<Rigidbody>();
        monsterBaseHitBox = GetComponentInChildren<ZombieHitBox>();

        // Set behavior state
        monsterBehaviorState = MonsterBehavior.Move;

        // Listen to HitBox events
        monsterBaseHitBox.OnPlayerEnterMonsterAttackRange += InRange;
        monsterBaseHitBox.OnPlayerExitMonsterAttackRange += OutOfRange;
    }

    // Reset after get monster out from pool
    public virtual void ResetMonsterState()
    {
        if (monsterBehaviorState == MonsterBehavior.Dead)
        {
            // Set behavior state
            monsterBehaviorState = MonsterBehavior.Move;

            // Listen to HitBox events
            monsterBaseHitBox.OnPlayerEnterMonsterAttackRange += InRange;
            monsterBaseHitBox.OnPlayerExitMonsterAttackRange += OutOfRange;
        }
    }

    // HANDLING MONSTER BEHAVIOR
    // Monster movement
    protected abstract void HandleMovement();

    // Monster attack
    // In range
    protected virtual void InRange()
    {
        // Set flag
        isPlayerInside = true;
        //
        ReadyToAttack();
    }

    // Ready attack state: This the pre attack state can be interupt when player out of range
    protected virtual void ReadyToAttack()
    {
        // Set behavior state
        monsterBehaviorState = MonsterBehavior.ReadyToAttack;
        // Handling coroutine
        if (readyAttackCoroutine == null)
        {
            readyAttackCoroutine = StartCoroutine(ReadyToAttackCoroutine());
        }

    }
    protected virtual IEnumerator ReadyToAttackCoroutine()
    {
        float elapsedTime = 0f;
        float waitTime = monsterStats.AttackSpeed * 0.7f;

        while (elapsedTime < waitTime)
        {
            if (monsterBehaviorState == MonsterBehavior.Move) 
            {   
                StopCoroutine(readyAttackCoroutine);
                readyAttackCoroutine = null;
                yield break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        monsterBehaviorState = MonsterBehavior.Attack;
        // Start coroutine
        if (attackCoroutine == null)
        {
            attackCoroutine = StartCoroutine(AttackCoroutine());
        }
    }

    // Attack state: This is the attack state can't be interupt untill monster attack
    public virtual void Attack()
    {
        if (isPlayerInside)
        {
            player.Hurt(monsterStats.Damage);
        }
    }
    protected virtual IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(monsterStats.AttackSpeed * 0.3f);
        HandleOnMonsterAttack();
    }
    public virtual void ResetAttack()
    {
        if (isPlayerInside == true)
        {
            readyAttackCoroutine = null;
            attackCoroutine = null;
            ReadyToAttack();
            return;
        }
        readyAttackCoroutine = null;
        attackCoroutine = null;
        monsterBehaviorState = MonsterBehavior.Move;
    }

    // OutOfRange
    protected virtual void OutOfRange()
    {
        isPlayerInside = false;
        if (monsterBehaviorState == MonsterBehavior.Attack) return;
        monsterBehaviorState = MonsterBehavior.Move;
    }

    // Monster get hurt
    public abstract void Hurt(float damageTaken);

    // Monster dead
    protected abstract void Dead();
    public virtual void DropExp()
    {
        // Initial values
        int dropAmount = Random.Range(1,2);
        GameObject expGemGameObject;
        ExpGem expGem;

        // Drop item
        for (int i = 0; i < dropAmount; i++)
        {
            expGemGameObject = ExpGemObjectPool.Instance.GetObject(this.transform );
            expGem = expGemGameObject.GetComponentInChildren<ExpGem>();
            expGem.LaunchItemRandomDirection();
        }
    }
    
    public virtual void DropCoin()
    {
        // Initial values
        int dropAmount = Random.Range(1,3);
        GameObject coinGameObject;
        Coin coin;

        // Drop item
        for (int i = 0; i < dropAmount; i++)
        {
            coinGameObject = CoinObjectPool.Instance.GetObject(this.transform);
            coin = coinGameObject.GetComponentInChildren<Coin>();
            coin.LaunchItemRandomDirection();
        }
    }

    // SUPPORT FUNCTIONS
    // Get special effect
    public abstract void ReceiveSpecialEffect(SpecialEffectBase specialEffect);

    // Invoke event
    protected void HandleOnMonsterAttack()
    {
        OnMonsterAttack?.Invoke();
    }
    protected void HandleOnMonsterHurt()
    {
        OnMonsterHurt?.Invoke();
    }
    protected void HandleOnMonsterDead()
    {
        OnMonsterDead?.Invoke(this, new OnMonsterDeadEventArgs{ monsterBaseController = this});
    }
}
