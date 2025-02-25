using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameplayManager : MonoBehaviour
{
    //
    public class OnSelectedWeaponEventArgs : EventArgs
    {
        public IWeapon weaponBase;
    }
    //

    

    public static GameplayManager Instance {get ;private set;}

    //
    [SerializeField] private Sword sword;  


    //
    public event EventHandler<OnSelectedWeaponEventArgs> OnSelectedWeapon;


    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Game Manager instance !");
        }
        Instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            SelecteWeapon(sword);
        }
    }

    public void SelecteWeapon(IWeapon weaponBase)
    {
        OnSelectedWeapon?.Invoke(this, new OnSelectedWeaponEventArgs{ weaponBase = weaponBase});
    }


}
