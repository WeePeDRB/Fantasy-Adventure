using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class HeroVFXController : MonoBehaviour
{
    // Controller reference
    protected HeroController heroController;

    // Dissolve VFX
    protected SkinnedMeshRenderer skinnedMesh;
    protected Material[] materials;
    protected float dissolveTime;

    // Initialize data
    protected virtual void InitializeData()
    {
        // Hero controller 
        heroController = GetComponentInParent<HeroController>();

        // Skin meshes
        skinnedMesh = GetComponentInChildren<SkinnedMeshRenderer>();

        // Mesh material
        materials = skinnedMesh.materials;

        // Dissolve time
        dissolveTime = 6f;
        
        // Event subscribe
        heroController.OnDead += HeroDead;
    }

    // Dead VFX 
    protected void HeroDead(HeroDead heroDead)
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
    protected abstract void ResetDissolveVFXValue();
}
