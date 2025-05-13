public class IncreaseResistance : BlessingBase
{
    //
    // CONTRUCTOR
    //

    public IncreaseResistance()
    {
        blessingName = "Increase Resistance";
        blessingLevel = 1;
    }

    //
    // FUNCTIONS
    //

    public override void ApplyBlessingOnHero(HeroBaseController hero)
    {
        hero.HeroStats.Resistance += 10;
    }
}