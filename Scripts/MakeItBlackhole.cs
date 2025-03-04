using UnityEngine;

public class MassBasedMaterialChange : MonoBehaviour
{
    public Material newMaterial; // Assign the new material in the Inspector
    private Rigidbody rb;
    private Material originalMaterial;
    private float originalMass;
    private bool hasChanged = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody missing on " + gameObject.name);
            return;
        }

        originalMass = rb.mass;
        originalMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        if (!hasChanged && rb.mass >= 4 * originalMass)
        {
            GetComponent<Renderer>().material = newMaterial;
            transform.localScale *= 3; // Double the size of the object
            hasChanged = true;
        }
    }
}
