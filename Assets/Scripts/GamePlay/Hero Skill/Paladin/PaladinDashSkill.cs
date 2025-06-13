using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PaladinDashSkill : SkillBase
{
    //
    // FIELDS
    //
    private PaladinController paladinController;
    private float dashDistance; // How far is the dash
    private float dashSpeed; // How fast is the dash
    [SerializeField]private ParticleSystem dashParticle;

    private Material testMaterial;
    [SerializeField] private SO_SpecialEffect resistanceBoostData;
    private ResistanceBoost resistanceBoost;

    public float DashDistance
    {
        get { return dashDistance; }
        set { dashDistance = value; }
    }
    public float DashSpeed
    {
        get { return dashSpeed; }
        set { dashSpeed = value; }
    }

    //
    // FUNCTIONS
    //

    protected override void InitializeSkillUniqueData()
    {
        paladinController = GetComponentInParent<PaladinController>();
        resistanceBoost = new ResistanceBoost(resistanceBoostData);

        paladinController.OnHeroDash += SkillActivate;
        
    }

    public override void SkillActivate()
    {
        dashParticle.Play();

        // Refresh special effect time remain
        resistanceBoost.Refresh();

        // Apply effect on paladin
        paladinController.ReceiveSpecialEffect(resistanceBoost);
    }

    private void Start()
    {
        InitializeSkillUniqueData();
    }
}
