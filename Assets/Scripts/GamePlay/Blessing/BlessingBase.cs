using System;
using UnityEngine;
[Serializable]
public abstract class BlessingBase
{
    //
    // FIELDS
    //
    protected string id;
    protected string blessingName;
    protected string blessingDescription;
    protected int blessingLevel;
    protected float blessingValue;
    protected Sprite blessingSprite;

    //
    // PROPERTIES
    // 
    public string ID { get { return id; } }
    public string BlessingName { get { return blessingName; } }
    public string BlessingDescription { get { return blessingDescription; }}
    public int BlessingLevel
    {
        get { return blessingLevel; }
        set { blessingLevel = Mathf.Max(1, blessingLevel); }
    }
    public float BlessingValue { get { return blessingValue; }}
    public Sprite BlessingSprite { get { return blessingSprite; }}

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
