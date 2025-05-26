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
        // Refresh special effect
        damageBoost.Refresh();

        //
        foreach (MonsterBaseController monster in monsterListInHitBox)
        {
            monster.Hurt(monster.MonsterStats.Health * 30 / 100);
        }
        foreach (HeroBaseController hero in heroListInRange)
        {
            hero.ReceiveSpecialEffect(damageBoost, damageBoostData);
        }
        paladinController.ReceiveSpecialEffect(damageBoost, damageBoostData);
        skillParticle.Play();
    }

    private void CheckIfMonsterDead(object sender, OnMonsterDeadEventArgs monsterDeadEventArgs)
    {
        monsterDeadEventArgs.monsterBaseController.OnMonsterDead -= CheckIfMonsterDead;
        for (int i = 0; i < monsterListInHitBox.Count; i ++)
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

            // Add monster to hit box list
            monsterListInHitBox.Add(monsterBaseController);

            // Subscribe to monster dead event (Remove monster from list incase monster die)
            monsterBaseController.OnMonsterDead += CheckIfMonsterDead;
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
