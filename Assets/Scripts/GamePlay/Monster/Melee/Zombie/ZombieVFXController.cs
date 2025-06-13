using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class ZombieVFXController : MonsterBaseVFXController
{

    private void Start()
    {
        GetData();
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.X))
    //     {
    //         StartCoroutine(DissolveVFXCoroutine());
    //     }
    // }
}
