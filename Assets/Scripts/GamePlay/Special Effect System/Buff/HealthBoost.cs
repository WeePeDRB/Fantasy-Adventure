[System.Serializable]
public class HealthBoost : SpecialEffectBaseOld
{
    //
    // CONSTRUCTOR
    //
    public HealthBoost(SO_SpecialEffect specialEffectData)
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
    public override void ApplyEffectOnHero(HeroBaseController hero)
    {
        hero.HeroStats.Health += spEffectValue;
    }
    // Remove effect to hero
    public override void RemoveEffectOnHero(HeroBaseController hero)
    {
        return;
    }

    // Apply effect to monster
    public override void ApplyEffectOnMonster(MonsterBaseController monster)
    {
        throw new System.NotImplementedException();
    }
    // Remove effect to monster
    public override void RemoveEffectOnMonster(MonsterBaseController monster)
    {
        throw new System.NotImplementedException();
    }
}
