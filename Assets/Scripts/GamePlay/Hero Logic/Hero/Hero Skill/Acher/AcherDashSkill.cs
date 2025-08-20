using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcherDashSkill : SkillBase
{
    //
    // FIELDS
    //

    // Reference
    private AcherControllerold acherController;
    private float dashDistance;
    private float dashSpeed;

    // Special effect
    [SerializeField] private SO_SpecialEffect damageBoostData;
    private DamageBoost damageBoost;
    [SerializeField] private SO_SpecialEffect damageBoostUltData;
    private DamageBoost damageBoostUlt;

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
        acherController = GetComponentInParent<AcherControllerold>();
        damageBoost = new DamageBoost(damageBoostData);
        damageBoostUlt = new DamageBoost(damageBoostUltData);
        acherController.OnHeroDash += SkillActivate;
    }
    public override void SkillActivate()
    {
        if (acherController.HyperInstict)
        {
            damageBoostUlt.Refresh();
            acherController.ReceiveSpecialEffect(damageBoostUlt);
        }
        else
        {
            damageBoost.Refresh();
            acherController.ReceiveSpecialEffect(damageBoost);
        }
    }

    private void Start()
    {
        InitializeSkillUniqueData();
    }
}
