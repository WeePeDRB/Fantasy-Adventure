[System.Serializable]
public class Se_ResistanceBoost : SpecialEffectBase
{
    //
    // CONTRUCTOR
    //
    public Se_ResistanceBoost(SO_SpecialEffect specialEffectData)
    {
        id = specialEffectData.id;
        spEffectName = specialEffectData.spEffectName;
        spEffectDescription = specialEffectData.spEffectDescription;
        spEffectDuration = specialEffectData.spEffectDuration;
        spEffectTimeRemaining = spEffectDuration;
        spEffectValue = specialEffectData.spEffectValue;
        spEffectSprite = specialEffectData.spEffectSprite;
        spEffectType = specialEffectData.spEffectType;
        spEffectTarget = specialEffectData.spEffectTarget;
    }

    //
    // FUNCTIONS
    // 

    // Apply effect to hero
    public override void ApplyEffectOnHero(HeroController heroController)
    {
        heroController.StatsController.ResistanceAddition += spEffectValue;
    }
    // Remove effect to hero
    public override void RemoveEffectFromHero(HeroController heroController)
    {
        if (heroController.StatsController.ResistanceAddition != 0) heroController.StatsController.ResistanceAddition -= spEffectValue;
    }

    // Apply effect to monster
    public override void ApplyEffectOnMonster(MonsterController monsterController)
    {

    }
    // Remove effect to monster
    public override void RemoveEffectFromMonster(MonsterController monsterController)
    {

    }
}
