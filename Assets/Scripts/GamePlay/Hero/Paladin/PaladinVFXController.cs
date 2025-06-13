using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinVFXController : HeroBaseVFXController
{
    protected override void GetData()
    {
        base.GetData();
        dissolveTime = 6f;
    }
    private void Start()
    {
        GetData();
    }
}
