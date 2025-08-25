using System.Collections.Generic;
using UnityEngine;

public class PaladinUltimateSkill : SkillBase
{
    //
    // FIELDS
    //

    // Reference
    private List<MonsterBaseControllerOld> monsterListInHitBox;
    private List<HeroBaseController> heroListInRange;
    private PaladinControllerOld paladinController;
    
    // VFX
    [SerializeField] private ParticleSystem ultimateParticle;

    // Special effect
    [SerializeField] private SO_SpecialEffect resistanceBoostData;
    [SerializeField] private SO_SpecialEffect healthBoostData;
   // private ResistanceBoost resistanceBoost;
   // private HealthBoost healthBoost;

    //
    // FUNCTIONS
    //

    protected override void InitializeSkillUniqueData()
    {
        // Initialize references
        monsterListInHitBox = new List<MonsterBaseControllerOld>();
        heroListInRange = new List<HeroBaseController>();
        paladinController = GetComponentInParent<PaladinControllerOld>();

        // Initialize special effect
       // resistanceBoost = new ResistanceBoost(resistanceBoostData);
       // healthBoost = new HealthBoost(healthBoostData);

    }

    public override void SkillActivate()
    {
        ultimateParticle.Play();

        // Refresh special effect time remain
        // resistanceBoost.Refresh();
        // healthBoost.Refresh();
        
        // // Apply special effect to other hero
        // foreach (HeroBaseController character in heroListInRange)
        // {
        //     character.ReceiveSpecialEffect(healthBoost);
        // }

        // // Apply special effect to paladin
        // paladinController.ReceiveSpecialEffect(resistanceBoost);
        // paladinController.ReceiveSpecialEffect(healthBoost);
    }

    //
    private void OnTriggerEneter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            monsterListInHitBox.Add(collider.gameObject.GetComponent<MonsterBaseControllerOld>());
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            MonsterBaseControllerOld monster = collider.gameObject.GetComponent<MonsterBaseControllerOld>();
            monsterListInHitBox.Remove(monster);
        }  
    }

    private void MonsterListControl(object sender, OnMonsterDeadEventArgs monster)
    {

    }

    // Start is called before the first frame update
    private void Start()
    {
        InitializeSkillUniqueData();
    }
}
