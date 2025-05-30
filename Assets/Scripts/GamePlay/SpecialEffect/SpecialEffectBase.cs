using UnityEngine;


public abstract class SpecialEffectBase
{
    //
    // FIELDS
    //

    // Essential information
    protected string id;
    protected string spEffectName;
    protected string spEffectDescription; 
    protected float spEffectDuration; 
    protected float spEffectTimeRemaining; 
    protected float spEffectValue;
    protected Sprite spEffectSprite;
    protected EffectType spEffectType;
    protected EffectTarget spEffectTarget;

    //
    // PROPERTIES
    //

    //
    public string ID { get { return id; } }
    public string SpEffectName { get { return spEffectName; } }
    public string SpEffectDescription { get { return spEffectDescription; } }
    public float SpEffectDuration { get { return spEffectDuration; } }
    public float SpEffectTimeRemaining { get { return spEffectTimeRemaining; } }
    public float SpEffectValue { get { return spEffectValue;  } }
    public Sprite SpEffectSprite { get { return spEffectSprite; }}
    public EffectType SpEffectType { get { return spEffectType; } }
    public EffectTarget SpEffectTarget { get { return spEffectTarget; } }

    //
    // FUNCTIONS
    //

    // MANAGE EFFECT LIFECYCLE
    // Update effect
    public void UpdateTime(float deltaTime)
    {
        spEffectTimeRemaining -= deltaTime;
    }

    // Refresh duration
    public void Refresh()
    {
        spEffectTimeRemaining = spEffectDuration; 
    }

    // Apply effect to hero
    public abstract void ApplyEffectOnHero(HeroBaseController hero);
    // Remove effect to hero
    public abstract void RemoveEffectOnHero(HeroBaseController hero);

    // Apply effect to monster
    public abstract void ApplyEffectOnMonster(MonsterBaseController monster);
    // Remove effect to monster
    public abstract void RemoveEffectOnMonster(MonsterBaseController monster);

}