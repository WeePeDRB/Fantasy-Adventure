public class IncreaseHealth : BlessingBase
{
    public IncreaseHealth(SO_Blessing blessingData)
    {
        id = blessingData.id;
        blessingName = blessingData.blessingName;
        blessingDescription = blessingData.blessingDescription;
        blessingLevel = blessingData.blessingLevel;
        blessingValue = blessingData.blessingValue;
        blessingSprite = blessingData.blessingSprite;
    }

    //
    // FUNCTIONS
    //

    public override void ApplyBlessingOnHero(HeroBaseController hero)
    {
        hero.HeroStats.MaxHealth += blessingValue;
        hero.HeroStats.Health += blessingValue;
    }
}