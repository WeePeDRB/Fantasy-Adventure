public abstract class BlessingBase
{
    //
    // FIELDS
    //

    // Essential information
    protected string blessingName;
    protected int blessingLevel;

    //
    // PROPERTIES
    // 

    //
    public string BlessingName { get { return blessingName; } }
    public int BlessingLevel { get { return blessingLevel; } }

    //
    // FUNCIONS
    //

    // Apply blessing to hero
    public abstract void ApplyBlessingOnHero(HeroBaseController hero);
    // Level up
    public virtual void BlessingLevelUp()
    {
        if (blessingLevel < 5) blessingLevel ++;
        
    }
}
