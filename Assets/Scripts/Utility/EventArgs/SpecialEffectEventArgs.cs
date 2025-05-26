using System;

public class OnReceiveSpecialEffectEventArgs : EventArgs
{
    public SO_SpecialEffect specialEffectData;
}

public class OnSpecialEffectEndEventArgs : EventArgs
{
    public UI_SpecialEffectComponent specialEffectComponent;
}