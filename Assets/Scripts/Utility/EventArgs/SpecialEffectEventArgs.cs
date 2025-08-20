using System;

public class OnReceiveSpecialEffectEventArgs : EventArgs
{
    public SpecialEffectBaseOld specialEffect;
}

public class OnSpecialEffectEndEventArgs : EventArgs
{
    public UI_SpecialEffectComponent specialEffectComponent;
}