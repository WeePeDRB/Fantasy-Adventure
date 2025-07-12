using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpGem : ItemBase
{
    private int expValue = 10;
    private GameObject hero;
    private bool nearHero;

    public void GemMove()
    {
        if (!nearHero) return;
        Vector3 newPos = Vector3.MoveTowards(transform.position, hero.transform.position, 8 * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    public void GetData(GameObject hero)
    {
        this.hero = hero;
        nearHero = true;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        nearHero = false;
    }

    private void FixedUpdate()
    {
        GemMove();
    }

    // Collider check
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HeroBaseController heroBaseController = collision.gameObject.GetComponent<HeroBaseController>();
            heroBaseController.GainExp(expValue);
            nearHero = false;
            ExpGemObjectPool.Instance.ReturnObject(gameObject);
        }
    }
}
