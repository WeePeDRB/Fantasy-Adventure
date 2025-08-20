[System.Serializable]
public class DamageBoost : SpecialEffectBaseOld
{
    //
    // CONSTRUCTOR
    //
    public DamageBoost(SO_SpecialEffect specialEffectData)
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
        hero.HeroStats.DamageAmplifierAddition += spEffectValue;
    }
    // Remove effect to hero
    public override void RemoveEffectOnHero(HeroBaseController hero)
    {
        if (hero.HeroStats.DamageAmplifierAddition != 0) hero.HeroStats.DamageAmplifierAddition -= spEffectValue;
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
