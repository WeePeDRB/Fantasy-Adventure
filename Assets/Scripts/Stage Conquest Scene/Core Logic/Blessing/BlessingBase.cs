using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class BlessingBase
{
    // Blessing data
    protected string id;
    public string ID { get { return id; } }

    protected string blessingName;
    public string BlessingName { get { return blessingName; } }

    protected string blessingDescription;
    public string BlessingDescription { get { return blessingDescription; } }

    protected int blessingLevel;
    public int BlessingLevel { get { return blessingLevel; } }

    protected float blessingValue;
    public float BlessingValue { get { return blessingValue; } }

    protected Sprite blessingSprite;
    public Sprite BlessingSprite { get { return blessingSprite; } }

    protected int blessingMaxLevel = 5;

    // Blessing level up
    public void BlessingLevelUp(HeroController heroController)
    {
        blessingLevel++;
        ApplyBlessingOnHero(heroController);
    }

    // Check if blessing reach max level
    public bool IsMaxLevel()
    {
        if (blessingLevel >= blessingMaxLevel) return true;
        return false;
    }

    // Apply blessing on hero
    public abstract void ApplyBlessingOnHero(HeroController heroController);
}
