using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AcherSpecialSkill : SkillBase
{
    //
    // FIELDS
    //

    // Reference
    private List<MonsterBaseControllerOld> monsterListInHitBox;
    private AcherControllerold acherController;
    private GameObject acherGameObject;
    private Vector3 worldPos;
    private Vector3 localPos;

    // VFX
    [SerializeField] private ParticleSystem skillParticle;

    //
    // FUNCTIONS
    //

    protected override void InitializeSkillUniqueData()
    {
        //
        monsterListInHitBox = new List<MonsterBaseControllerOld>();
        acherController = GetComponentInParent<AcherControllerold>();

        //
        acherGameObject = acherController.gameObject;
        localPos = transform.localPosition;

        //
    }

    public override void SkillActivate()
    {
        StartCoroutine(SkillApplyDamage());
    }
    private IEnumerator SkillApplyDamage()
    {
        GetWorldPosition();
        skillParticle.Play();
        float time = 0f;
        while (time <= 3)
        {
            foreach (MonsterBaseControllerOld monster in monsterListInHitBox.ToList())
            {
                if (monster != null)
                {
                    float damage = Mathf.Round(monster.MonsterStats.Health * 10 / 100);
                    if (damage < 10)
                    {
                        damage = 10;
                    }
                    monster.Hurt(damage);
                }
            }
            yield return new WaitForSeconds(1f);
            time++;
        }
        skillParticle.Stop();
        // Delay small amount of time for the animation smoothness 
        yield return new WaitForSeconds(1.8f);
        GetLocalPosition();
        monsterListInHitBox.Clear();
    }

    // Convert to World space
    private void GetWorldPosition()
    {
        worldPos = transform.parent.TransformPoint(transform.localPosition);
        transform.SetParent(null);
        transform.position = worldPos;
    }
    // Convert to Local space
    private void GetLocalPosition()
    {
        transform.SetParent(acherGameObject.transform);
        transform.localPosition = localPos;
    }
    //

    // Checking monster avaiable in monster list
    private void CheckIfMonsterDead(object sender, OnMonsterDeadEventArgs monsterDeadEventArgs)
    {
        monsterDeadEventArgs.monsterBaseController.OnMonsterDead -= CheckIfMonsterDead;
        for (int i = 0; i < monsterListInHitBox.Count; i++)
        {
            if (monsterListInHitBox[i] == monsterDeadEventArgs.monsterBaseController)
            {
                monsterListInHitBox.Remove(monsterListInHitBox[i]);
            }
        }
    }
    //
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            MonsterBaseControllerOld monsterBaseController = collider.gameObject.GetComponent<MonsterBaseControllerOld>();
            monsterListInHitBox.Add(monsterBaseController);
            monsterBaseController.OnMonsterDead += CheckIfMonsterDead;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Monster"))
        {
            MonsterBaseControllerOld monsterBaseController = collider.gameObject.GetComponent<MonsterBaseControllerOld>();
            monsterListInHitBox.Remove(monsterBaseController);
            monsterBaseController.OnMonsterDead -= CheckIfMonsterDead;
        }
    }

    private void Start()
    {
        InitializeSkillUniqueData();
    }
}
