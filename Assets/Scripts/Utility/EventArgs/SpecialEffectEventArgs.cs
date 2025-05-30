using System;

public class OnReceiveSpecialEffectEventArgs : EventArgs
{
    public SpecialEffectBase specialEffect;
}

public class OnSpecialEffectEndEventArgs : EventArgs
{
    public UI_SpecialEffectComponent specialEffectComponent;
}