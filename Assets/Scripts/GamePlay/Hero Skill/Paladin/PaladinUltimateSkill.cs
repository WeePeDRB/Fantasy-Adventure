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
    [SerializeField] private SO_SpecialEffect resistanceBoostData;
    [SerializeField] private SO_SpecialEffect healthBoostData;
    private ResistanceBoost resistanceBoost;
    private HealthBoost healthBoost;

    //
    // FUNCTIONS
    //

    // Instantiate skill
    protected override void InitializeSkill()
    {
        // Instantiate references
        monsterListInHitBox = new List<MonsterBaseController>();
        heroListInRange = new List<HeroBaseController>();
        paladinController = GetComponentInParent<PaladinController>();

        // Initialize special effect
        resistanceBoost = new ResistanceBoost(resistanceBoostData);
        healthBoost = new HealthBoost(healthBoostData);

        //
        paladinController.OnHeroUltimate += SkillActivate;
    }

    // Activate skill
    public override void SkillActivate()
    {
        // Refresh special effect
        resistanceBoost.Refresh();
        healthBoost.Refresh();
        
        // Apply special effect to other hero
        foreach (HeroBaseController character in heroListInRange)
        {
            character.ReceiveSpecialEffect(healthBoost, healthBoostData);
        }

        // Apply special effect to paladin
        paladinController.ReceiveSpecialEffect(resistanceBoost, resistanceBoostData);
        paladinController.ReceiveSpecialEffect(healthBoost, healthBoostData);
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
        InitializeSkill();
    }
}
