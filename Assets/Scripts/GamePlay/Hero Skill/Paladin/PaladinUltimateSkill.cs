using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinUltimateSkill : SkillBase
{
    //
    // FIELDS
    //

    // Reference
    private List<MonsterBaseController> monsterListInHitBox;
    private List<HeroBaseController> heroListInRange;
    private PaladinController paladinController;


    // Special effect
    private ResistanceBoost resistanceBoost;
    private HealthBoost healthBoost;
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
        resistanceBoost = new ResistanceBoost(100, 10f, EffectTarget.Character);
        healthBoost = new HealthBoost(0.1f, 8f, EffectTarget.Character);
        damageBoost = new DamageBoost(50 , 10f, EffectTarget.Character);

        //
        paladinController.OnHeroUltimate += SkillActivate;
    }

    // Activate skill
    protected override void SkillActivate()
    {
        // Apply special effect to other hero
        foreach (HeroBaseController character in heroListInRange)
        {
            character.ReceiveSpecialEffect(healthBoost);
        }

        // Apply special effect to paladin
        paladinController.ReceiveSpecialEffect(resistanceBoost);
        paladinController.ReceiveSpecialEffect(damageBoost);
        paladinController.ReceiveSpecialEffect(healthBoost);
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
