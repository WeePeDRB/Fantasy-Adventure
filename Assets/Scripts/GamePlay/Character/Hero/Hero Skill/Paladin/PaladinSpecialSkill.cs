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

    // VFX
    public ParticleSystem skillParticle;

    // Special effect
    [SerializeField] private SO_SpecialEffect damageBoostData;
    private DamageBoost damageBoost;

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
        damageBoost = new DamageBoost(damageBoostData);
    }

    public override void SkillActivate()
    {
        // Refresh special effect time remain
        damageBoost.Refresh();

        //
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

    // Checking monster avaiable in monster list
    private void CheckIfMonsterDead(object sender, OnMonsterDeadEventArgs monsterDeadEventArgs)
    {
        monsterDeadEventArgs.monsterBaseController.OnMonsterDead -= CheckIfMonsterDead;
        for (int i = 0; i < monsterListInHitBox.Count; i++)
        {
            if (monsterListInHitBox[i] == monsterDeadEventArgs.monsterBaseController)
            {
                monsterListInHitBox.Remove(monsterListInHitBox[i]);
            }
        }
    }
    //
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            MonsterBaseController monsterBaseController = collider.gameObject.GetComponent<MonsterBaseController>();
            monsterListInHitBox.Add(monsterBaseController);
            monsterBaseController.OnMonsterDead += CheckIfMonsterDead;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            MonsterBaseController monsterBaseController = collider.gameObject.GetComponent<MonsterBaseController>();
            monsterListInHitBox.Remove(monsterBaseController);
            monsterBaseController.OnMonsterDead -= CheckIfMonsterDead;
        }  
    }

    // Start is called before the first frame update
    private void Start()
    {
        InitializeSkillUniqueData();
    }

}
