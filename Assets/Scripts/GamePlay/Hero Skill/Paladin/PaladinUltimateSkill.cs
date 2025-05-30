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

    protected override void InitializeSkillUniqueData()
    {
        // Initialize references
        monsterListInHitBox = new List<MonsterBaseController>();
        heroListInRange = new List<HeroBaseController>();
        paladinController = GetComponentInParent<PaladinController>();

        // Initialize special effect
        resistanceBoost = new ResistanceBoost(resistanceBoostData);
        healthBoost = new HealthBoost(healthBoostData);

        //
        paladinController.OnHeroUltimate += SkillActivate;
    }

    public override void SkillActivate()
    {
        // Refresh special effect time remain
        resistanceBoost.Refresh();
        healthBoost.Refresh();
        
        // Apply special effect to other hero
        foreach (HeroBaseController character in heroListInRange)
        {
            character.ReceiveSpecialEffect(healthBoost);
        }

        // Apply special effect to paladin
        paladinController.ReceiveSpecialEffect(resistanceBoost);
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
    private void Start()
    {
        InitializeSkillUniqueData();
    }
}
