using System.Collections.Generic;
using UnityEngine;
using System;
public class PaladinSkill2 : HeroSkill
{
    // Paladin reference
    private PaladinController paladinController;

    // Skill data
    [SerializeField] private SO_HeroSkill heroSkillData;

    // Special effect
    [SerializeField] private SO_SpecialEffect critChanceBoostData;
    private Se_CritChanceBoost critChanceBoost;

    // AOE interact 
    private List<MonsterController> monstersInRange;
    private List<HeroController> herosInRange;
    
    // Initialize data
    protected override void InitializeData(SO_HeroSkill heroSkill)
    {
        base.InitializeData(heroSkill);

        // Paladin reference
        paladinController = GetComponentInParent<PaladinController>();

        // Initialize character list
        monstersInRange = new List<MonsterController>();
        herosInRange = new List<HeroController>();

        // Initialize special effects
        critChanceBoost = new Se_CritChanceBoost(critChanceBoostData);
    }

    public override void SkillActivate()
    {
        // Apply effect on paladin
        paladinController.SpecialEffectController.ReceiveEffect(critChanceBoost);

        // Apply effect on ally 
        foreach (HeroController heroController in herosInRange)
        {
            heroController.SpecialEffectController.ReceiveEffect(critChanceBoost);
        }

        // Apply effect on monster 
        foreach (MonsterController monsterController in monstersInRange)
        {
            // Calculate monster damage
            float damage = monsterController.StatsController.MaxHealth * 20 / 100;

            // Apply damage
            monsterController.TakeDamage(damage, paladinController);
        }

        // Invoke play VFX event        
        PlaySkillVFX();
    }
    // Character list checking
    // Collider checking
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            MonsterController monsteController = collider.gameObject.GetComponent<MonsterController>();
            monstersInRange.Add(monsteController);
            monsteController.OnMonsterDead += OnMonsterDead;
        }
        if (collider.gameObject.CompareTag("Player"))
        {
            HeroController heroController = collider.gameObject.GetComponent<HeroController>();
            herosInRange.Add(heroController);
            heroController.OnDead += OnHeroDead;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            MonsterController monsterController = collider.gameObject.GetComponent<MonsterController>();
            monstersInRange.Remove(monsterController);
            monsterController.OnMonsterDead -= OnMonsterDead;
        }
        if (collider.gameObject.CompareTag("Player"))
        {
            HeroController heroController = collider.gameObject.GetComponent<HeroController>();
            herosInRange.Add(heroController);
            heroController.OnDead -= OnHeroDead;
        }
    }
    // Check if monster is dead
    private void OnMonsterDead(MonsterDead monster)
    {
        // Unsub monster dead event when monster die
        monster.monsterController.OnMonsterDead -= OnMonsterDead;

        // Check for monster in monster list and remove it 
        for (int i = 0; i < monstersInRange.Count; i++)
        {
            if (monstersInRange[i] == monster.monsterController)
            {
                monstersInRange.Remove(monstersInRange[i]);
                return;
            }
        }
    }
    // Check if hero is dead
    private void OnHeroDead(HeroDead hero)
    {
        hero.heroController.OnDead -= OnHeroDead;

        for (int i = 0; i < herosInRange.Count; i++)
        {
            if (herosInRange[i] == hero.heroController)
            {
                herosInRange.Remove(herosInRange[i]);
                return;
            }            
        }
    }

    //
    private void Start()
    {
        InitializeData(heroSkillData);
    }
}
