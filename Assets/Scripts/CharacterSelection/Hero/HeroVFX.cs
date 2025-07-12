using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HeroVFX : MonoBehaviour
{
    // Selection checking    
    private string id;
    private bool isDissolve;
    private float dissolveTime;
    private Coroutine dissolveCoroutine;
    private Coroutine emergeCoroutine;

    // Dissolve VFX
    private Material[] materials;
    [SerializeField] private List<SkinnedMeshRenderer> skinMeshList;

    public void GetData(CharacterSelection characterSelection, SO_Hero heroData)
    {
        //
        //materials = skinnedMesh.materials;
        dissolveTime = 0.5f;
        isDissolve = false;
        id = heroData.id;

        //
        characterSelection.OnChangeHero += OnChangeHero;
    }

    private void OnChangeHero(object sender, HeroData heroData)
    {
        if (id == heroData.heroData.id)
        {
            if (isDissolve)
            {
                if (emergeCoroutine == null)
                {
                    emergeCoroutine = StartCoroutine(EmergeCoroutine());
                }
            }
        }
        else
        {
            if (!isDissolve)
            {
                if (dissolveCoroutine == null)
                {
                    dissolveCoroutine = StartCoroutine(DissolveCoroutine());
                }
                return;
            }
            return;
        }
    }

    protected IEnumerator DissolveCoroutine()
    {
        float elapsedTime = 0;

        //
        while (elapsedTime < dissolveTime)
        {
            foreach (SkinnedMeshRenderer skinMesh in skinMeshList)
            {
                materials = skinMesh.materials;
                for (int i = 0; i < materials.Count(); i++)
                {
                    materials[i].SetFloat("_DissolveAmount", elapsedTime / dissolveTime);
                }
            }
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        //
        isDissolve = true;
        dissolveCoroutine = null;
    }

    protected IEnumerator EmergeCoroutine()
    {
        float elapsedTime = dissolveTime;

        //
        while (elapsedTime > 0)
        {
            foreach (SkinnedMeshRenderer skinMesh in skinMeshList)
            {
                materials = skinMesh.materials;
                for (int i = 0; i < materials.Count(); i++)
                {
                    materials[i].SetFloat("_DissolveAmount", elapsedTime / dissolveTime);
                }
            }
            elapsedTime -= Time.deltaTime;
            yield return null;
        }

        //
        isDissolve = false;
        emergeCoroutine = null;
    }
}
