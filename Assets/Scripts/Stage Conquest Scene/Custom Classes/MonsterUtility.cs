using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonsterUtility
{
    // Get hero list in stage
    public static List<HeroController> InitializeHeroList()
    {
        // Get all game object with tag "Player"
        GameObject[] heroObjects = GameObject.FindGameObjectsWithTag("Player");
        List<HeroController> heroList = new List<HeroController>();

        foreach (GameObject heroObject in heroObjects)
        {
            HeroController hero = heroObject.GetComponent<HeroController>();
            if (hero != null)
            {
                heroList.Add(hero);
            }
        }
        return heroList;
    }

    // Find the closest hero
    public static HeroController FindClosestHero(List<HeroController> heroList, MonsterController monsterBaseController)
    {
        // Return null if hero list is empty
        if (heroList == null || heroList.Count == 0) return null;

        // Set up data for distance comparison.
        HeroController closestHero = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = monsterBaseController.gameObject.transform.position;

        foreach (HeroController hero in heroList)
        {
            // If hero is dead -> Skip and continue the loop.
            if (hero.HealthState == HeroHealthState.Dead) continue;

            // Compare distance
            float distance = Vector3.SqrMagnitude(hero.gameObject.transform.position - currentPosition);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestHero = hero;
            }
        }
        return closestHero;
    }

    // Damage pop up
    public static void DamagePopUp(float damageTaken, MonsterController monsterController)
    {
        // Pop up text
        GameObject text = TextPopUpObjectPool.Instance.GetObject(monsterController.transform);
        TextPopUp textPopUp = text.GetComponent<TextPopUp>();
        textPopUp.ResetTextPopUp();
        text.GetComponent<TextMesh>().text = damageTaken.ToString();
    }
}
