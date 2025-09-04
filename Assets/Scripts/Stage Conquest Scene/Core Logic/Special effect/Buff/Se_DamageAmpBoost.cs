[System.Serializable]
public class Se_DamageAmpBoost : SpecialEffectBase
{
    public Se_DamageAmpBoost(SO_SpecialEffect specialEffectData)
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

    public Se_DamageAmpBoost(SpecialEffectBase specialEffect)
    {
        id = specialEffect.ID;
        spEffectName = specialEffect.SpEffectName;
        spEffectDescription = specialEffect.SpEffectDescription;
        spEffectDuration = specialEffect.SpEffectDuration;
        spEffectTimeRemaining = specialEffect.SpEffectTimeRemaining;
        spEffectValue = specialEffect.SpEffectValue;
        spEffectSprite = specialEffect.SpEffectSprite;
        spEffectType = specialEffect.SpEffectType;
        spEffectTarget = specialEffect.SpEffectTarget;
    }

    // Clone
    public override SpecialEffectBase Clone(SpecialEffectBase specialEffect)
    {
        return new Se_DamageAmpBoost(specialEffect);
    }

    // Apply effect to hero
    public override void ApplyEffectOnHero(HeroController heroController)
    {
        heroController.StatsController.DamageAmplifierAddition += spEffectValue;
    }
    // Remove effect to hero
    public override void RemoveEffectFromHero(HeroController heroController)
    {
        if (heroController.StatsController.DamageAmplifierAddition != 0) heroController.StatsController.DamageAmplifierAddition -= spEffectValue;
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
