using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinUltimate : SkillBase
{
    //
    // FIELDS
    //

    // Reference
    private List<MonsterBaseController> monsterListInHitBox;
    private List<HeroBaseController> heroListInRange;

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
        heroBaseController = GetComponentInParent<HeroBaseController>();

        // Instantiate special effect
        resistanceBoost = new ResistanceBoost(100, 10f, EffectTarget.Character);
        healthBoost = new HealthBoost(5, 10f, EffectTarget.Character);
        damageBoost = new DamageBoost(50 , 10f, EffectTarget.Character);
    }

    // Activate skill
    protected override void SkillActivate()
    {
        foreach (HeroBaseController character in heroListInRange)
        {
            character.ReceiveSpecialEffect(healthBoost);
        }
        heroBaseController.ReceiveSpecialEffect(resistanceBoost);
        heroBaseController.ReceiveSpecialEffect(damageBoost);
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
