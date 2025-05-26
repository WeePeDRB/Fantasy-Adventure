using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPopUp : MonoBehaviour
{
    private float destroyTime = 2f;
    private Vector3 offset = new Vector3(0, 2f, 0);
    private Vector3 randomizeIntensity = new Vector3(0.5f, 0, 0);

    public void ResetTextPopUp()
    {
        transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        transform.localPosition += offset;
        transform.localPosition += new Vector3(Random.Range(-randomizeIntensity.x, randomizeIntensity.x),
        Random.Range(-randomizeIntensity.y, randomizeIntensity.y),
        Random.Range(-randomizeIntensity.z, randomizeIntensity.z));
        StartCoroutine(ReturnPoolCoroutine());
    }

    private IEnumerator ReturnPoolCoroutine()
    {
        yield return new WaitForSeconds(destroyTime);
        TextPopUpObjectPool.Instance.ReturnObject(gameObject);
    }
}
