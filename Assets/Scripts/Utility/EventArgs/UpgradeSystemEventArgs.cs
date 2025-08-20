using System;
using System.Collections.Generic;

// System seclect random upgrade
public class OnRandomUpgradeEventArgs : EventArgs
{
    public List<UpgradeData> randomUpgradeList;
}

// These event args only send a static data which is SO_
public class WeaponDataEventArgs : EventArgs
{
    public SO_Weapon weaponData;
}
public class BlessingDataEventArgs : EventArgs
{
    public SO_Blessing blessingData;
}

// These event args send the actual weapon reference that store in the hero weapon system
public class WeaponEventArgs : EventArgs
{
    public WeaponBaseOld weapon;
}
public class BlessingEventArgs : EventArgs
{
    public BlessingBaseOld blessing;
}

// These event args use to store weapon list when reach max quantity
public class WeaponListEventArgs : EventArgs
{
    public List<SO_Weapon> weaponDataList;
}
public class BlessingListEventArgs : EventArgs
{
    public List<SO_Blessing> blessingDataList;
}