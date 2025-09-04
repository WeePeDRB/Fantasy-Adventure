using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinVFXController : HeroVFXController
{
    // Skill particles
    // Skill 1
    [SerializeField] private ParticleSystem skill1Particle;
    [SerializeField] private HeroSkill skill1;
    // Skill 2
    [SerializeField] private ParticleSystem skill2Particle;
    [SerializeField] private HeroSkill skill2;
    // Skill 3
    [SerializeField] private ParticleSystem skill3Particle;
    [SerializeField] private HeroSkill skill3;

    // Initialize data
    protected override void InitializeData()
    {
        base.InitializeData();

        // Event subscribe
        skill1.playSkillVFX += PlaySkill1Particle;
        skill2.playSkillVFX += PlaySkill2Particle;
        skill3.playSkillVFX += PlaySkill3Particle;
    }

    // VFX control
    private void PlaySkill1Particle()
    {
        skill1Particle.Play();
    }
    private void PlaySkill2Particle()
    {
        skill2Particle.Play();
    }
    private void PlaySkill3Particle()
    {
        skill3Particle.Play();
    }
    protected override void ResetDissolveVFXValue()
    {
        throw new System.NotImplementedException();
    }

    // 
    private void Start()
    {
        InitializeData();
    }
}
