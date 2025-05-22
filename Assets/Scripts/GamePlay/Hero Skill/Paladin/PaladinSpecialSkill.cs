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
    public ParticleSystem skillParticle;

    // Special effect
    [SerializeField] private SO_SpecialEffect damageBoostData;
    private DamageBoost damageBoost;

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
        damageBoost = new DamageBoost(damageBoostData);
    }

    // Activate skill
    public override void SkillActivate()
    {
        Debug.Log("special skill activate");
        foreach (MonsterBaseController monster in monsterListInHitBox)
        {
            monster.Hurt(monster.MonsterStats.Health * 30 / 100);
        }
        foreach (HeroBaseController hero in heroListInRange)
        {
            hero.ReceiveSpecialEffect(damageBoost);
        }
        paladinController.ReceiveSpecialEffect(damageBoost);
        skillParticle.Play();
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
