using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    //Enemy basic stats
    protected float maxHealth;
    protected float health;
    protected float speed;


    //Enemy movement control
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
    protected abstract void InstantiateCharacter();

    //

}
