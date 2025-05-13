public class IncreaseHealth : BlessingBase
{
    //
    // CONTRUCTOR
    //

    public IncreaseHealth()
    {
        blessingName = "Increase Health";
        blessingLevel = 1;
    }

    //
    // FUNCTIONS
    //

    public override void ApplyBlessingOnHero(HeroBaseController hero)
    {
        hero.HeroStats.MaxHealth += 50;
    }
}