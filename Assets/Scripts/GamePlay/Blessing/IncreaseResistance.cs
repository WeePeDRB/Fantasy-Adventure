public class IncreaseResistance : BlessingBase
{
    public IncreaseResistance()
    {

    }

    public IncreaseResistance(SO_Blessing blessingData)
    {
        id = blessingData.id;
        blessingName = blessingData.blessingName;
        blessingLevel = blessingData.blessingLevel;
        blessingValue = blessingData.blessingValue;
    }
    //
    // FUNCTIONS
    //

    public override void ApplyBlessingOnHero(HeroBaseController hero)
    {
        hero.HeroStats.Resistance += blessingValue;
    }
}