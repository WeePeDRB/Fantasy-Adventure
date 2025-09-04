using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonsterController : MonoBehaviour
{
    // Monster stats management
    [SerializeField] protected SO_MonsterStats statsData;
    protected MonsterStatsController statsController;
    public MonsterStatsController StatsController
    {
        get { return statsController; }
    }

    // Monster special effect management
    protected MonsterSpecialEffectController specialEffectController;
    public MonsterSpecialEffectController SpecialEffectController
    {
        get { return specialEffectController; }
    }

    // Monster physics management
    protected Rigidbody rb;
    protected CapsuleCollider bodyCollider;

    // Monster state management
    protected MonsterBehaviorState behaviorState;
    public MonsterBehaviorState BehaviorState
    {
        get { return behaviorState; }
    }
    protected MonsterHealthState healthState;
    public MonsterHealthState HealthState
    {
        get { return healthState; }
    }

    // Hero detect function
    protected List<HeroController> heroList;
    protected HeroController heroTarget;

    // Attack function
    protected MonsterHitBox hitBox;
    protected bool isHeroInHitBox;
    protected bool isReadyToAttack;
    protected bool isAttacking;
    protected Coroutine attackCoroutine;
    public event Action OnMonsterAttack;

    // Dead & Reset state function
    public event Action<MonsterDead> OnMonsterDead;

    // Initialize data
    // This function will be called at the first time when instantiate monster object.
    // It main funtion is to set up monster's logic reference
    public void InitializeData()
    {
        // Initialize stats controller
        statsController = new MonsterStatsController(statsData);

        // Initialize special effect controller 
        specialEffectController = new MonsterSpecialEffectController(this);

        // Health state
        healthState = MonsterHealthState.Alive;

        // Behavior state
        behaviorState = MonsterBehaviorState.Moving;

        // Physics value 
        rb = GetComponent<Rigidbody>();
        bodyCollider = GetComponent<CapsuleCollider>();

        // Attack functions
        hitBox = GetComponent<MonsterHitBox>();
        hitBox.OnHeroEnterRange += InRange;
        hitBox.OnHeroExitRange += OutOfRange;
        isReadyToAttack = true;
        heroList = MonsterUtility.InitializeHeroList();
    }

    // Reset monster's data. 
    // This function will be called each time the monster is retrieved from the object pool. 
    // It's function is to reset its values after being disabled when the monster dies.
    public void ResetMonsterState()
    {
        if (healthState == MonsterHealthState.Dead)
        {
            // Health state
            healthState = MonsterHealthState.Alive;

            // Behavior state
            behaviorState = MonsterBehaviorState.Moving;

            // Physics value
            rb.useGravity = true;
            bodyCollider.enabled = true;

            // Attack functions
            hitBox.OnHeroEnterRange += InRange;
            hitBox.OnHeroExitRange += OutOfRange;
            isReadyToAttack = true;
        }
    }


    // Monster special effect 
    protected void UpdateSpecialEffect()
    {
        if (specialEffectController.IsDictionaryEmpty()) return;
        specialEffectController.UpdateEffect(Time.deltaTime);
    }

    // Monster movement
    protected void HandleMovement()
    {
        //Specify direction
        Vector3 direction = (heroTarget.transform.position - this.transform.position).normalized;
        Vector3 moveDirVector = new Vector3(direction.x, 0, direction.z);
        //Rotation
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirVector, Time.deltaTime * rotateSpeed);

        if (behaviorState == MonsterBehaviorState.Moving)
        {
            //Movement
            Vector3 targetPos = transform.position + moveDirVector * statsController.Speed * Time.deltaTime;
            rb.MovePosition(targetPos);
        }
    }

    // Hero detection
    protected IEnumerator DetectClosestHero()
    {
        // Check if monster is still alive 
        // If yes -> Continuously search for the nearest hero.
        while (healthState == MonsterHealthState.Alive)
        {
            yield return new WaitForSeconds(1f);
            heroTarget = MonsterUtility.FindClosestHero(heroList, this);
        }
    }

    // Monster attack
    protected virtual void Attack()
    {
        // Invoke the attack event
        OnMonsterAttack?.Invoke();
        isReadyToAttack = false;
        isAttacking = true;
    }
    
    // This function is used to apply damage to the hero within the monster’s attack range.
    // The function is set to public because I want it to be called from the animator 
    // to synchronize with the attack animation (triggered right when the animation ends).
    public abstract void ApplyDamage(HeroController heroController);

    // This function is used to create the cooldown for the attack function.
    // The function is also set to public so that it can be called in the last frame of the attack animation.
    public IEnumerator AttackRecover()
    {
        isAttacking = false;
        yield return new WaitForSeconds(statsController.AttackSpeed);
        isReadyToAttack = true;
    }
    protected virtual void InRange()
    {
        // Set hero is in attack range
        isHeroInHitBox = true;
    }
    protected virtual void OutOfRange()
    {
        // Set hero is out of range
        isHeroInHitBox = false;
    }

    // Monster hurt & dead

    // This function is to check if the damage source (hero) is still alive (or in normal condition)
    // and if so, applies its effect on the monster.
    public void TakeDamage(float damageTake, HeroController heroController)
    {
        if (heroController.HealthState == HeroHealthState.Alive)
        {
            Hurt(damageTake);
        }
    }

    protected void Hurt(float damageTaken)
    {
        if (healthState == MonsterHealthState.Alive)
        {
            float damageAfterResistance;

            // Calculate the remaining damage after applying the Hero's resistance stat.
            damageAfterResistance = damageTaken - (damageTaken * statsController.Resistance / 100f);

            // Apply damange
            statsController.CurrentHealth -= damageAfterResistance;

            // Damage pop up
            MonsterUtility.DamagePopUp(damageAfterResistance, this);

            // Check if hero's health reach 0 -> Dead
            if (statsController.CurrentHealth == 0) Dead();
        }
    }
    protected void Dead()
    {
        // Health state
        healthState = MonsterHealthState.Dead;

        // Physics value
        rb.useGravity = false;
        bodyCollider.enabled = false;

        // Events unsub
        hitBox.OnHeroEnterRange -= InRange;
        hitBox.OnHeroExitRange -= OutOfRange;

        // Invoke dead event
        OnMonsterDead?.Invoke(new MonsterDead { monsterController = this });
    }

    // Monster behavior control
    protected void BehaviorControl()
    {
        if (healthState == MonsterHealthState.Alive)
        {
            if (behaviorState == MonsterBehaviorState.Attacking)
            {
                if (isReadyToAttack)
                {
                    Attack();
                }
            }
            else if (behaviorState == MonsterBehaviorState.Idling)
            {
                if (isAttacking) return;
                else 
                {
                    behaviorState = MonsterBehaviorState.Moving;
                }
            }
        }
    }
}
