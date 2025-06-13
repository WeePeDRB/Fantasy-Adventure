using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroBaseVFXController : MonoBehaviour
{
    // State checking
    protected HeroBaseController controller;

    // Dissolve VFX
    protected float dissolveTime;
    protected SkinnedMeshRenderer skinMesh;
    protected Material[] materials;

    protected virtual void GetData()
    {
        controller = GetComponentInParent<HeroBaseController>();
        skinMesh = GetComponentInChildren<SkinnedMeshRenderer>();
        materials = skinMesh.materials;

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

        controller.OnHeroDead += HeroDead;
    }

    protected void HeroDead()
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
}
