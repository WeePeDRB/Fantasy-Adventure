public class ResistanceBoost : SpecialEffectBase
{
    //
    // CONTRUCTOR
    //
    public ResistanceBoost(SO_SpecialEffect specialEffectData)
    {
        id = specialEffectData.id;
        effectName = specialEffectData.specialEffectName;
        duration = specialEffectData.specialEffectDuration;
        timeRemaining = duration;
        effectTarget = specialEffectData.specialEffectTarget;
        value = specialEffectData.specialEffectValue;
        effectType = EffectType.Instant;
    }

    //
    // FUNCTIONS
    // 

    // Apply effect to hero
    public override void ApplyEffectOnHero(HeroBaseController hero)
    {
        hero.HeroStats.ResistanceAddition += value;
    }
    // Remove effect to hero
    public override void RemoveEffectOnHero(HeroBaseController hero)
    {
        if (hero.HeroStats.ResistanceAddition != 0) hero.HeroStats.ResistanceAddition -= value;
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
