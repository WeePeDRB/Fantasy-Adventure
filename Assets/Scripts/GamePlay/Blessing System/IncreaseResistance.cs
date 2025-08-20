public class IncreaseResistance : BlessingBaseOld
{
    public IncreaseResistance(SO_Blessing blessingData)
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
        hero.HeroStats.ResistanceBase += blessingValue;
    }
}