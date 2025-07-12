using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcherUltimateSkill : SkillBase
{
    //
    // FIELDS
    //

    // Reference 
    private AcherController acherController;

    // Special effect
    [SerializeField] private SO_SpecialEffect speedBoostData;
    private SpeedBoost speedBoost;

    [SerializeField] private ParticleSystem ultimateParticle;

    //
    // FUNCTIONS
    //

    protected override void InitializeSkillUniqueData()
    {
        acherController = GetComponentInParent<AcherController>();
        speedBoost = new SpeedBoost(speedBoostData);
        acherController.OnHeroUltimate += SkillActivate;
    }
    public override void SkillActivate()
    {
        // Refresh special effect time remain
        speedBoost.Refresh();

        // Apply special effect to acher
        acherController.ReceiveSpecialEffect(speedBoost);

        //
        StartCoroutine(ParticleControl());
    }

    private IEnumerator ParticleControl()
    {
        ultimateParticle.Play();
        yield return new WaitForSeconds(10f);
        ultimateParticle.Stop();
    }

    private void Start()
    {
        InitializeSkillUniqueData();
    }
}
