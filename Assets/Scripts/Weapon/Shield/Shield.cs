using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IWeapon
{
    //Weapon colliders
    [SerializeField] private GameObject weaponHitBox;
    [SerializeField] private GameObject weaponParryBox;


    //Weapon stats
    private     float   weaponAttackSpeed;
    private     float   weaponAttackDamage;
    private     int     weaponLevel;
    
    //
    [SerializeField] private Paladin player;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.SetParent(player.forwardPosition.transform);
        this.transform.localPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    public void Attack()
    {

    }

    public void EquipWeapon()
    {

    }

    public void UpgradeWeapon()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            
        }
    }
}
