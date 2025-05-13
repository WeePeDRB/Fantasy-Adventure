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

    // MONSTER STATE
    protected MonsterMovementState monsterMovementState;
    protected MonsterAttackState monsterAttackState;
    protected MonsterHealthState monsterHealthState;

    // CHECKING FLAGS
    protected bool isPlayerInsideAttackHitBox;
    protected bool isPlayerInsideAttackRange;

    // MONSTER STATS
    protected MonsterStats monsterStats;

    // MONSTER EFFECT STATUS
    protected MonsterEffectStatus monsterEffectStatus;

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
    protected HeroBaseController heroTarget;
    protected List<HeroBaseController> heroList;
    
    // Custom class for event args
    public class OnMonsterDeadEventArgs : EventArgs
    {
        public MonsterBaseController monsterBaseController;
    }

    //
    // PROPERTIES
    //
    public HeroBaseController HeroTarget
    {
        get { return heroTarget; }
    }
    public SO_Monster MonsterData
    {
        get { return monsterData; }
    }
    public MonsterStats MonsterStats
    {
        get { return monsterStats; }
    }
    public MonsterMovementState MonsterMovementState 
    { 
        get { return monsterMovementState; } 
    }
    public MonsterAttackState MonsterAttackState
    {
        get { return monsterAttackState; }
    }
    public MonsterHealthState MonsterHealthState
    {
        get { return monsterHealthState; }
    }

    // INITIAL SET UP FOR MONSTER
    // First set up for monster
    public virtual void InstantiateMonster()
    {
        // Set variables
        monsterCollider = GetComponent<CapsuleCollider>();
        monsterRigidbody = GetComponent<Rigidbody>();
        monsterBaseHitBox = GetComponentInChildren<MonsterBaseHitBox>();

        // Set monster state
        monsterMovementState = MonsterMovementState.Move;
        monsterAttackState = MonsterAttackState.Standby;
        monsterHealthState = MonsterHealthState.Alive;

        // Listen to HitBox events
        monsterBaseHitBox.OnPlayerEnterMonsterAttackRange += InRange;
        monsterBaseHitBox.OnPlayerExitMonsterAttackRange += OutOfRange;

        //
        heroList = new List<HeroBaseController>();
    }

    // Reset after get monster out from pool
    public virtual void ResetMonsterState()
    {  
        // Enable the collider
        monsterCollider.enabled = true;

        // Rigidbody
        monsterRigidbody.useGravity = true;

        // Subscribe HitBox events
        monsterBaseHitBox.OnPlayerEnterMonsterAttackRange += InRange;
        monsterBaseHitBox.OnPlayerExitMonsterAttackRange += OutOfRange;
            
        // Set behavior state
        monsterMovementState = MonsterMovementState.Move;
        monsterAttackState = MonsterAttackState.Standby;
        monsterHealthState = MonsterHealthState.Alive;

        // Set health
        monsterStats.Health = monsterStats.MaxHealth;
    }

    // HANDLING MONSTER BEHAVIOR
    // Monster movement
    protected virtual void HandleMovement()
    {
        if (monsterMovementState == MonsterMovementState.Move)
        {
            //Specify direction
            Vector3 direction = (heroTarget.transform.position - this.transform.position).normalized;
            Vector3 moveDirVector = new Vector3(direction.x, 0, direction.z);

            //Movement
            Vector3 targetPos = transform.position + moveDirVector * monsterStats.Speed * Time.deltaTime;
            monsterRigidbody.MovePosition(targetPos);
        

            //Rotation
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirVector, Time.deltaTime * rotateSpeed);
        }
    }
    // Monster rotation
    protected virtual void HandleRotation()
    {
        if (monsterMovementState == MonsterMovementState.Rotate)
        {
            //Specify direction
            Vector3 direction = (heroTarget.transform.position - this.transform.position).normalized;
            Vector3 moveDirVector = new Vector3(direction.x, 0, direction.z);
            
            //Rotation
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirVector, Time.deltaTime * rotateSpeed);
        }
    }

    // Monster attack
    // Checking hit box
    protected abstract void InRange();
    protected abstract void OutOfRange();
    // Attack process    
    protected abstract void ReadyToAttack();
    protected abstract IEnumerator ReadyToAttackCoroutine();
    protected abstract IEnumerator AttackCoroutine();
    public abstract void ApplyDamage(HeroBaseController heroHit);
    public abstract void ResetAttack();

    // Monster get hurt
    public virtual void Hurt(float damageTaken)
    {
        if (monsterHealthState == MonsterHealthState.Alive)
        {
            monsterStats.Health -= damageTaken;

            if (monsterStats.Health <= 0)
            {
                Dead();
            }
        }
    }

    // Monster dead
    protected virtual void Dead()
    {

        // Disable collider
        monsterCollider.enabled = false;

        // Disable gravity in rigidbody
        monsterRigidbody.useGravity = false;

        // Invoke dead event
        HandleOnMonsterDead();

        // Unsubscribe HitBox events
        monsterBaseHitBox.OnPlayerEnterMonsterAttackRange -= InRange;
        monsterBaseHitBox.OnPlayerExitMonsterAttackRange -= OutOfRange;
            
        // Set behavior state
        monsterHealthState = MonsterHealthState.Dead;
        
    }

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
    // Special effect handling
    // Receive special effect
    public virtual void ReceiveSpecialEffect(SpecialEffectBase specialEffect)
    {

    }
    // Update special effect
    public virtual void UpdateSpecialEffect()
    {
        if (monsterEffectStatus.IsDictionaryEmpty())
        {
            return;
        }
        monsterEffectStatus.UpdateEffects(Time.deltaTime);
    }

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

    // Distance checking for behavior state
    protected void DistanceCheck(float requreDistance)
    {
        Vector3 heroPosition = heroTarget.transform.position;
        
        float distance = Vector3.Distance(heroPosition, transform.position);

        if (distance <= requreDistance)
        {
            isPlayerInsideAttackRange = true;
        }
        else 
        {
            isPlayerInsideAttackRange = false; 
        }
    }

    public void UpdateHeroTarget()
    {
        heroTarget = GameUtility.FindClosestHero(heroList, this);
    }

    // 
    public void GetHeroList(List<HeroBaseController> heroList)
    {
        this.heroList = heroList;
    }
}
