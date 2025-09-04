using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcherVFXController : HeroVFXController
{
    // Skill particles

    // Skill 2
    [SerializeField] private ParticleSystem skill2Particle;
    [SerializeField] private AcherSkill2 skill2;
    // Skill 3
    [SerializeField] private ParticleSystem skill3Particle;
    [SerializeField] private AcherSkill3 skill3;

    // Initialize data
    protected override void InitializeData()
    {
        base.InitializeData();

        // Event subscribe
        // Skill 2
        skill2.playSkillVFX += PlaySkill2Particle;
        skill2.stopSkillVFX += StopSkill2Particle;
        // Skill 3
        skill3.playSkillVFX += PlaySkill3Particle;
        skill3.stopSkillVFX += StopSkill3Particle;
    }

    // VFX control
    // Skill 2
    private void PlaySkill2Particle()
    {
        skill2Particle.Play();
    }
    private void StopSkill2Particle()
    {
        skill2Particle.Stop();
    }
    // Skill 3
    private void PlaySkill3Particle()
    {
        skill3Particle.Play();
    }
    private void StopSkill3Particle()
    {
        skill3Particle.Stop();
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
