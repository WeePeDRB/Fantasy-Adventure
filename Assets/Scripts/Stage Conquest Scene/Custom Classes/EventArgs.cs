using System;
using System.Collections.Generic;

//
// Hero Weapon controller event args
//
public class Weapon : EventArgs
{
    public WeaponBase weapon;
}
public class WeaponList : EventArgs
{
    public List<WeaponBase> weaponList;
}

//
// Hero Blessing controller event args
//
public class Blessing : EventArgs
{
    public BlessingBase blessing;
}
public class BlessingList : EventArgs
{
    public List<BlessingBase> blessingList;
}

//
// Hero Special effect controller event args
//
public class SpecialEffect : EventArgs
{
    public SpecialEffectBase specialEffect;
}

//
// Monster controller event args
//
public class MonsterDead : EventArgs
{
    public MonsterController monsterController;
}

//
// Melee monster hit box event args
//
public class HerosInRange : EventArgs
{
    public List<HeroController> herosInRange;
}