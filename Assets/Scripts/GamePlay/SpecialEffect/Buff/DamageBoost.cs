public class DamageBoost : SpecialEffectBase
{
    //
    // FIELDS
    //

    private float damageAmplifier;
    public float DamageAmplifier
    {
        get { return damageAmplifier; }
    }
    
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
        damageAmplifier = specialEffectData.specialEffectValue;
    }

    //
    // FUNCTIONS
    //

    // Apply effect to hero
    public override void ApplyEffectOnHero(HeroBaseController hero)
    {
        hero.HeroStats.DamageAmplifier = damageAmplifier;
    }
    // Remove effect to hero
    public override void RemoveEffectOnHero(HeroBaseController hero)
    {
        
        hero.HeroStats.DamageAmplifier = hero.HeroData.damageAmplifier;
    }


    // Apply effect to monster
    public override void ApplyEffectOnMonster(MonsterBaseController monster)
    {
        monster.MonsterStats.DamageAmplifier = damageAmplifier;
    }
    // Remove effect to monster
    public override void RemoveEffectOnMonster(MonsterBaseController monster)
    {
        
    }
}
