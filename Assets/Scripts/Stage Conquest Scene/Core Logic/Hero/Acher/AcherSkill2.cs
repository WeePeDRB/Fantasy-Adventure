using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcherSkill2 : HeroSkill
{
    // Acher reference
    private AcherController acherController;

    // Skill data
    [SerializeField] private SO_HeroSkill heroSkillData;

    // AOE interact
    private List<MonsterController> monstersInRange;

    // Hit box position
    private Vector3 localPos;

    // Skill duration
    private float skillActiveDuration = 3f;

    // Initialize data
    protected override void InitializeData(SO_HeroSkill heroSkill)
    {
        base.InitializeData(heroSkill);

        // Acher reference
        acherController = GetComponentInParent<AcherController>();

        // Initialize character list
        monstersInRange = new List<MonsterController>();

        // Position initialize
        localPos = transform.localPosition;
    }

    public override void SkillActivate()
    {
        // Start skill
        StartCoroutine(SkillCountDown());

        // Invoke play VFX event
        PlaySkillVFX();
    }

    // Skill count down
    private IEnumerator SkillCountDown()
    {
        // Convert to world space
        GetWorldPosition();

        // Apply damage logic
        float time = 0f;
        while (time <= skillActiveDuration)
        {
            foreach (MonsterController monster in monstersInRange)
            {
                if (monster.HealthState == MonsterHealthState.Alive)
                {
                    // Calculate the ammount of damange the skill will deal
                    // The damage dealt by the skill will be 5% of the monster’s max health per second.
                    float damage = monster.StatsController.MaxHealth * 5 / 100;

                    monster.TakeDamage(damage, acherController);
                }
            }
            yield return new WaitForSeconds(1f);
            time++;
        }

        // Invoke stop VFX event
        StopSkillVFX();
        
        // Delay small amount of time for the animation smoothness 
        yield return new WaitForSeconds(1.8f);
        GetLocalPosition();
        // Return to local space
        GetLocalPosition();

        // Erase the hit box list
        monstersInRange.Clear();
    }

    // Position convert
    // World space position
    private void GetWorldPosition()
    {
        // Separate the hit box to the parent object
        transform.SetParent(null);

        // 
    }
    // Local space position
    private void GetLocalPosition()
    {
        // Set the parent back for hit box
        transform.SetParent(acherController.gameObject.transform);

        // Return to the local postion
        transform.localPosition = localPos;
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
    }
        private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            MonsterController monsterController = collider.gameObject.GetComponent<MonsterController>();
            monstersInRange.Remove(monsterController);
            monsterController.OnMonsterDead -= OnMonsterDead;
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
    // 
    private void Start()
    {
        InitializeData(heroSkillData);
    }
}
