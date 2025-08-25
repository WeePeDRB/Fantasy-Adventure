using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public abstract class MonsterBaseControllerOld : MonoBehaviour
{ 
    //
    // FIELDS
    //

    // MONSTER DATA
    [SerializeField] protected SO_Monster monsterData;

    // MONSTER STATE
    protected MonsterBehaviorStateOld monsterBehaviorState;
    protected MonsterHealthStateOld monsterHealthState;

    // CHECKING FLAGS
    protected bool isPlayerInsideAttackHitBox;
    protected bool isReadyToAttack;
    protected bool isAttacking;

    // MONSTER STATS
    protected MonsterStats monsterStats;

    // MONSTER EFFECT STATUS
    protected MonsterSpecialEffectSystem monsterSpecialEffectSystem;

    // MONSTER BEHAVIOR EVENTS
    public event Action OnMonsterVFXReset;
    public event Action OnMonsterAttack;

    public event EventHandler<OnMonsterDeadEventArgs> OnMonsterDead;

    // MONSTER PHYSICS 
    protected Rigidbody monsterRigidbody;
    protected CapsuleCollider monsterCollider;

    // REFERENCE 
    protected MonsterBaseHitBox monsterBaseHitBox;
    protected HeroBaseController heroTarget;
    protected List<HeroBaseController> heroList;

    protected float standbyCountdown;

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
    public MonsterBehaviorStateOld MonsterBehaviorState 
    { 
        get { return monsterBehaviorState; } 
    }
    public MonsterHealthStateOld MonsterHealthState
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
        monsterBehaviorState = MonsterBehaviorStateOld.Move;
        monsterHealthState = MonsterHealthStateOld.Alive;

        // Listen to HitBox events
        monsterBaseHitBox.OnPlayerEnterMonsterAttackRange += InRange;
        monsterBaseHitBox.OnPlayerExitMonsterAttackRange += OutOfRange;

        //
        isReadyToAttack = true;
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

        // Set monster state
        monsterBehaviorState = MonsterBehaviorStateOld.Move;
        monsterHealthState = MonsterHealthStateOld.Alive;

        // 
        isReadyToAttack = true;
        isAttacking = false;

        // Set health
        monsterStats.Health = monsterStats.MaxHealth;

        // Reset VFX state
        OnMonsterVFXReset?.Invoke();
    }

    // HANDLING MONSTER BEHAVIOR

    // Monster behavior controller
    protected abstract void InRange();
    protected abstract void OutOfRange();
    protected abstract void BehaviorController();

    // Monster movement
    protected virtual void HandleMovement()
    {
        //Specify direction
        Vector3 direction = (heroTarget.transform.position - transform.position).normalized;
        Vector3 moveDirVector = new Vector3(direction.x, 0, direction.z);
        //Rotation
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirVector, Time.deltaTime * rotateSpeed);

        if (monsterBehaviorState == MonsterBehaviorStateOld.Move)
        {
            //Movement
            Vector3 targetPos = transform.position + moveDirVector * monsterStats.Speed * Time.deltaTime;
            monsterRigidbody.MovePosition(targetPos);
        }
    }

    // Monster attack
    // Attack process    
    protected abstract void Attack();
    public abstract IEnumerator AttackRecover();
    public abstract void ApplyDamage(HeroBaseController heroHit);

    // Monster get hurt
    public virtual void Hurt(float damageTaken)
    {
        if (monsterHealthState == MonsterHealthStateOld.Alive)
        {
            // Pop up text
            GameObject text = TextPopUpObjectPool.Instance.GetObject(this.transform);
            TextPopUp textPopUp = text.GetComponent<TextPopUp>();
            textPopUp.ResetTextPopUp();
            text.GetComponent<TextMesh>().text = damageTaken.ToString();

            // Take damage logic
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
        monsterHealthState = MonsterHealthStateOld.Dead;
    }
    public virtual void DropExp()
    {
        // Initial values
        int dropAmount = Random.Range(1,2);

        // Drop item
        for (int i = 0; i < dropAmount; i++)
        {
            ExpGemObjectPool.Instance.GetObject(this.transform );
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
            
        }
    }

    //
    protected void StandbyStateBreak()
    {
        if (monsterBehaviorState == MonsterBehaviorStateOld.Standby)
        {
            standbyCountdown += Time.deltaTime;
            if (standbyCountdown >= 2)
            {
                monsterBehaviorState = MonsterBehaviorStateOld.Move;
                standbyCountdown = 0f;
            }
        }
        else
        {
            standbyCountdown = 0f;
        }
    }

    // SUPPORT FUNCTIONS
    // Special effect handling
    // public virtual void ReceiveSpecialEffect(SpecialEffectBaseOld specialEffect)
    // {

    // }
    public virtual void UpdateSpecialEffect()
    {
        if (monsterSpecialEffectSystem.IsDictionaryEmpty())
        {
            return;
        }
        monsterSpecialEffectSystem.UpdateEffectsTime(Time.deltaTime);
    }

    // Invoke event
    protected void HandleOnMonsterAttack()
    {
        OnMonsterAttack?.Invoke();
    }
    protected void HandleOnMonsterDead()
    {
        OnMonsterDead?.Invoke(this, new OnMonsterDeadEventArgs{ monsterBaseController = this});
    }

    //
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
