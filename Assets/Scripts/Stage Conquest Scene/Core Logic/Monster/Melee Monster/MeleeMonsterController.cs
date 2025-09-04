using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeMonsterController : MonsterController
{
    // Monster attack
    public override void ApplyDamage(HeroController heroController)
    {
        // Check if hero is still in hit box incase hero escapse in last moment
        if (isHeroInHitBox)
        {
            heroTarget.Hurt(statsController.AttackDamage);
        }
    }

    // For melee monsters, I will use the "BehaviorControl" logic combined with body collision detection 
    // to control the monster’s actions. 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            behaviorState = MonsterBehaviorState.Attacking;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            behaviorState = MonsterBehaviorState.Idling;
        }
    }
}
