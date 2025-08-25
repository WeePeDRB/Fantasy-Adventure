using System;
using System.Collections.Generic;

// Monster state event args
public class OnMonsterDeadEventArgs : EventArgs
{
    public MonsterBaseControllerOld monsterBaseController;
}

// Monster projectile event args
public class OnWitchProjectileHitEventArgs : EventArgs
{
    public HeroBaseController heroBaseController;
    public WitchProjectile witchProjectile;
}

