[System.Serializable]
public abstract class SpecialEffectBase
{
    //
    // FIELDS
    //

    // Essential information
    protected string id;
    protected string effectName; // Effect name
    protected float duration; // Effect duration
    protected float timeRemaining; // Time remaining 
    protected EffectTarget effectTarget;

    //
    // PROPERTIES
    //

    //
    public string ID { get { return id; } }
    public string EffectName { get { return effectName; } }
    public float TimeRemaining { get { return timeRemaining; } }
    public float Duration { get { return duration; } }
    public EffectTarget EffectTarget { get { return effectTarget; } }

    //
    // FUNCTIONS
    //

    // MANAGE EFFECT LIFECYCLE
    // Update effect
    public void UpdateTime(float deltaTime)
    {
        timeRemaining -= deltaTime;
    }

    // Refresh duration
    public void Refresh()
    {
        timeRemaining = duration;
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