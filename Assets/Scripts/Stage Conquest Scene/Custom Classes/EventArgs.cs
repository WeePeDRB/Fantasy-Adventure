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
// Hero controller event args
//
public class HeroDead : EventArgs
{
    public HeroController heroController;
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

//
// Range monster projectile 
//
public class MonsterProjectile : EventArgs
{
    public HeroController heroController;
    public Projectile monsterProjectile;
}