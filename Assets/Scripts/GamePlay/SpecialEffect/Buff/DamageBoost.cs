public class DamageBoost : SpecialEffectBase
{
    //
    // CONSTRUCTOR
    //
    public DamageBoost(SO_SpecialEffect specialEffectData)
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
        hero.HeroStats.DamageAmplifierAddition += value;
    }
    // Remove effect to hero
    public override void RemoveEffectOnHero(HeroBaseController hero)
    {
        if (hero.HeroStats.DamageAmplifierAddition != 0) hero.HeroStats.DamageAmplifierAddition -= value;
    }


    // Apply effect to monster
    public override void ApplyEffectOnMonster(MonsterBaseController monster)
    {
       //monster.MonsterStats.DamageAmplifier = value;
    }
    // Remove effect to monster
    public override void RemoveEffectOnMonster(MonsterBaseController monster)
    {
        
    }
}
