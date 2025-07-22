using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameUtility
{
    // Manage hero
    // Initialize hero list
    public static List<HeroBaseController> InitializeHeroList()
    {
        GameObject[] heroObjects = GameObject.FindGameObjectsWithTag("Player");
        List<HeroBaseController> heroList = new List<HeroBaseController>();

        foreach (var obj in heroObjects)
        {
            HeroBaseController hero = obj.GetComponent<HeroBaseController>();
            if (hero != null)
            {
                heroList.Add(hero);
            }
        }
        return heroList;
    }
    // Find closest hero
    public static HeroBaseController FindClosestHero(List<HeroBaseController> heroList, MonsterBaseController monsterBaseController)
    {
        if (heroList == null || heroList.Count == 0) return null;

        HeroBaseController closestHero = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = monsterBaseController.gameObject.transform.position;

        foreach (var hero in heroList)
        {
            if (hero == null) continue;

            float distance = Vector3.SqrMagnitude(hero.gameObject.transform.position - currentPosition);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestHero = hero;
            }
        }
        return closestHero;
    }

    // Manager
    public static void PauseGame()
    {
        Time.timeScale = 0;
    }

    public static void UnpauseGame()
    {
        Time.timeScale = 1;
    }
}
