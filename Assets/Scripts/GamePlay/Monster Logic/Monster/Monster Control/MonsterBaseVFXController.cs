using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.VFX;

public class MonsterBaseVFXController : MonoBehaviour
{
    // State checking 
    protected MonsterBaseControllerOld controller;

    // Dissolve VFX 
    protected float dissolveTime;
    protected SkinnedMeshRenderer skinMesh;
    protected Material[] materials;
    [SerializeField] protected VisualEffect dissolveVFX;

    protected void GetData()
    {
        controller = GetComponentInParent<MonsterBaseControllerOld>();
        skinMesh = GetComponentInChildren<SkinnedMeshRenderer>();
        materials = skinMesh.materials;
        dissolveTime = 4f;
        if (controller == null)
        {
            Debug.LogError("Controller is null !");
            return;
        }
        if (skinMesh == null)
        {
            Debug.LogError("Mesh is null !");
            return;
        }
        if (materials == null)
        {
            Debug.LogError("Material is null !");
            return;
        }

        controller.OnMonsterDead += MonsterDead;
        controller.OnMonsterVFXReset += ResetVFX;
    }

    protected void MonsterDead(object sender, OnMonsterDeadEventArgs onMonsterDeadEventArgs)
    {
        StartCoroutine(DissolveVFXCoroutine());
    }

    protected IEnumerator DissolveVFXCoroutine()
    {
        float elapsedTime = 0;
        while (elapsedTime < dissolveTime)
            {
                for (int i = 0; i < materials.Count(); i++)
                {
                    materials[i].SetFloat("_DissolveAmount", elapsedTime / dissolveTime);
                }
                elapsedTime += Time.deltaTime;
                yield return null;
            }
    }

    public void ResetVFX()
    {
        // Reset dissolve materials
        for (int i = 0; i < materials.Count(); i++)
        {
            materials[i].SetFloat("_DissolveAmount", 0);
        }
    }
}
