using System.Collections;
using UnityEngine;

public class DisolveEffCall : MonoBehaviour
{

    public float dissolveDuration = 2;
    public float dissolveStrength;

    
    void Start()
    {
        Material dissolveMaterial = GetComponent<Renderer>().sharedMaterial;
        dissolveMaterial.SetFloat("_DisolveLevel", 0);
    }

    public void StartDissolver()
    {
        StartCoroutine(Dissolver());
    }

    public IEnumerator Dissolver()
    {
        float elapsedTime = 0;
        Material dissolveMaterial = GetComponent<Renderer>().sharedMaterial;

        if (!dissolveMaterial.HasProperty("_DisolveLevel"))
        {
            Debug.LogError("Shader does not have a property named '_DisolveLevel'");
            yield break;
        }

        while (elapsedTime < 3f) // Change duration to 3 seconds
        {
            elapsedTime += Time.deltaTime;
            dissolveStrength = Mathf.Lerp(0, 0.75f, elapsedTime / 3f); // Smoothly interpolate from 0 to 0.75
            dissolveMaterial.SetFloat("_DisolveLevel", dissolveStrength);
            yield return null;
        }

        dissolveMaterial.SetFloat("_DisolveLevel", 0.75f); // Ensure final value is set
    }

    public void setItBackToOriginalDissolve()
    {
        Material dissolveMaterial = GetComponent<Renderer>().sharedMaterial;
        dissolveMaterial.SetFloat("_DisolveLevel", 0);
    }



    void Update()
    {
        
    }
}
