using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class HeroVFXController : MonoBehaviour
{
    // Controller reference
    protected CharacterController characterController;

    // Dissolve VFX
    protected float dissolveTime;
    protected SkinnedMeshRenderer skinnedMesh;
    protected Material[] materials;

    // Initialize data
    protected abstract void InitializeVFXController();

    // Dissolve VFX control
    protected abstract void StartDissolveVFX();
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
