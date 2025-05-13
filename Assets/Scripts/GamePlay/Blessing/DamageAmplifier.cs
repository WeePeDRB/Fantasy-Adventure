public class DamageAmplifier : BlessingBase
{
    //
    // CONTRUCTOR
    //

    public DamageAmplifier()
    {
        blessingName = "Damage Amplifier";
        blessingLevel = 1;
    }

    //
    // FUNCTIONS
    //

    public override void ApplyBlessingOnHero(HeroBaseController hero)
    {
        hero.HeroStats.DamageAmplifier += 10;
    }
}