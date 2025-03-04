using UnityEngine;
using System.Collections;

public class DisolveEffCall : MonoBehaviour
{
    public Material dissolveMaterial; // Assign the dissolve material in the Inspector
    public float dissolveSpeed = 2.0f;
    private float dissolveValue = 0f;

    void Start()
    {
        if (dissolveMaterial != null)
            dissolveMaterial.SetFloat("_DissolveAmount", 0); // Reset to default
    }

    public void StartDissolver()
    {
        StopAllCoroutines(); 
        StartCoroutine(DissolveEffect());
    }

    public void setItBackToOriginalDissolve()
    {
        StopAllCoroutines(); 
        StartCoroutine(RevertDissolve());
    }

    IEnumerator DissolveEffect()
    {
        while (dissolveValue < 1)
        {
            dissolveValue += Time.deltaTime * dissolveSpeed;
            if (dissolveMaterial != null)
                dissolveMaterial.SetFloat("_DissolveAmount", dissolveValue);

            yield return null;
        }
    }

    IEnumerator RevertDissolve()
    {
        while (dissolveValue > 0)
        {
            dissolveValue -= Time.deltaTime * dissolveSpeed;
            if (dissolveMaterial != null)
                dissolveMaterial.SetFloat("_DissolveAmount", dissolveValue);

            yield return null;
        }
    }
}
