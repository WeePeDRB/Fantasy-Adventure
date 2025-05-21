using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinSpecialSkill : SkillBase
{
    //
    // FIELDS
    //

    // Reference
    private List<MonsterBaseController> monsterListInHitBox;
    private List<HeroBaseController> heroListInRange;
    private PaladinController paladinController;

    // Special effect
    private DamageBoost damageBoost;

    //
    // FUNCTIONS
    //

    // Instantiate skill
    protected override void InstantiateSkill()
    {
        // Instantiate references
        monsterListInHitBox = new List<MonsterBaseController>();
        heroListInRange = new List<HeroBaseController>();
        paladinController = GetComponentInParent<PaladinController>();

        // Instantiate special effect
        damageBoost = new DamageBoost(20, 10f, EffectTarget.Hero);

        //
        paladinController.OnHeroSpecial += SkillActivate;
    }

    // Activate skill
    protected override void SkillActivate()
    {
        foreach (MonsterBaseController monster in monsterListInHitBox)
        {
            monster.Hurt(monster.MonsterStats.Health * 30 / 100);
        }
        foreach (HeroBaseController hero in heroListInRange)
        {
            hero.ReceiveSpecialEffect(damageBoost);
        }
    }

    //
    private void OnTriggerEneter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            monsterListInHitBox.Add(collider.gameObject.GetComponent<MonsterBaseController>());
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            monsterListInHitBox.Remove(collider.gameObject.GetComponent<MonsterBaseController>());
        }  
    }

    // Start is called before the first frame update
    void Start()
    {
        InstantiateSkill();
    }

}
