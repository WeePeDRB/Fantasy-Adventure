using UnityEngine;

public abstract class BlessingBase
{
    //
    // FIELDS
    //

    // Essential information
    protected string id;
    protected string blessingName;
    protected int blessingLevel;
    protected float blessingValue;

    //
    // PROPERTIES
    // 

    //
    public string ID { get { return id; } }
    public string BlessingName { get { return blessingName; } }
    public int BlessingLevel 
    { 
        get { return blessingLevel; } 
        set { blessingLevel = Mathf.Max(1, blessingLevel);}    
    }

    //
    // FUNCIONS
    //
    public abstract void ApplyBlessingOnHero(HeroBaseController hero);
    public virtual void BlessingLevelUp(HeroBaseController hero)
    {
        if (blessingLevel < 5)
        {
            blessingLevel ++;
            ApplyBlessingOnHero(hero);
        }
    }
}
