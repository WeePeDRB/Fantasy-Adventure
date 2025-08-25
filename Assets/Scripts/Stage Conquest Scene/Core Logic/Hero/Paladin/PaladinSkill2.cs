using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinSkill2 : HeroSkill
{
    // Paladin reference
    private PaladinController paladinController;

    // AOE interact 
    private List<MonsterBaseControllerOld> monsterInRange;
    private List<HeroController> heroInRange;

    // Special effect
    public override void SkillActivate()
    {
        throw new System.NotImplementedException();
    }
}
