using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    //Enemy basic stats
    protected float maxHealth;
    protected float health;
    protected float speed;
    protected float attackSpeed;

    //
    protected bool isReady; //Ready to move
    protected bool isReadyToAttack; //Ready to attack
    

    //Return the isReady bool to true
    //This function will run in the end of the stand up animation
    public void EnemyReady()
    {
        isReady = true;
    }


    //Enemy movement control
    //This function will check the direction to move to player and make player move
    protected virtual void HandleMovement()
    {
        //Specify direction
        Vector3 direction = (CharacterBase.Instance.transform.position - this.transform.position).normalized;
        Vector3 moveDirVector = new Vector3(direction.x, 0, direction.z);
        Debug.Log("Enemy move direction : " + moveDirVector);
        //Movement
        transform.position += moveDirVector * speed * Time.deltaTime;

        //Rotation
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDirVector, Time.deltaTime * rotateSpeed);
    }

    //Instantiate enemy 
    //Instantiate the stats for enemy
    protected abstract void InstantiateCharacter();

    //Attack function
    protected abstract void IsReadyToAttack();
    protected abstract void Attack();
}
