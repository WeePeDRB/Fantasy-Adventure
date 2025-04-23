using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonsterBaseController
{
    //
    // FIELDS
    //

    //
    // PROPERTIES
    //

    public MonsterBehavior MonsterBeHaviorState { get { return monsterBehaviorState; } }


    //
    // FUNCTIONS
    //

    // INITIAL SET UP FOR ZOMBIE
    public override void InstantiateMonster()
    {
        base.InstantiateMonster();

        // Initial stats for zombie
        monsterStats = new MonsterStats(100,2,1,10,0.5f,0,0);

    }

    

    // HANDLING ZOMBIE BEHAVIOR
    // Zombie movement
    protected override void HandleMovement()
    {
        if (monsterBehaviorState == MonsterBehavior.Move)
        {
            //Specify direction
            Vector3 direction = (player.transform.position - this.transform.position).normalized;
            Vector3 moveDirVector = new Vector3(direction.x, 0, direction.z);

            //Movement
            transform.position += moveDirVector * monsterStats.Speed * Time.deltaTime;

            //Rotation
            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDirVector, Time.deltaTime * rotateSpeed);
        }
    }

    // Zombie attack

    // Zombie get hurt
    public override void Hurt(float damageTaken)
    {
        if (monsterBehaviorState != MonsterBehavior.Dead)
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
        // Disable collider
        monsterCollider.enabled = false;

        // Rigidbody
        monsterRigidbody.useGravity = false;

        // Invoke dead event
        HandleOnMonsterDead();

        // Unsub all event
        monsterBaseHitBox.OnPlayerEnterMonsterAttackRange -= ReadyToAttack;
        monsterBaseHitBox.OnPlayerExitMonsterAttackRange -= OutOfRange;
        
        //
        monsterBehaviorState = MonsterBehavior.Dead;

        // Drop item when dead
        DropExp();
        DropCoin();
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
