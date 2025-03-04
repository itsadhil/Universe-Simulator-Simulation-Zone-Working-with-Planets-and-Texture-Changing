using UnityEngine;

public class PlanetOrbit : MonoBehaviour
{
    [Header("Orbit Settings")]
    public Transform sun;
    public float orbitSpeed = 10f;
    public float orbitRadius = 10f;
    public float selfRotationSpeed = 50f;

    private float currentOrbitAngle = 0f;
    private bool isGrabbed = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (sun != null)
        {
            // Calculate initial angle based on position
            Vector3 offset = transform.position - sun.position;
            currentOrbitAngle = Mathf.Atan2(offset.z, offset.x) * Mathf.Rad2Deg;
        }
    }

    void Update()
    {
        if (!isGrabbed && sun != null)
        {
            // Update orbit angle based on speed
            currentOrbitAngle += orbitSpeed * Time.deltaTime;

            // Calculate new position using angle
            float x = sun.position.x + Mathf.Cos(currentOrbitAngle * Mathf.Deg2Rad) * orbitRadius;
            float z = sun.position.z + Mathf.Sin(currentOrbitAngle * Mathf.Deg2Rad) * orbitRadius;
            transform.position = new Vector3(x, transform.position.y, z);
        }

        // Self-rotation
        transform.Rotate(Vector3.up, selfRotationSpeed * Time.deltaTime);
    }

    public void Grabbed()
    {
        isGrabbed = true;
        if (rb != null) rb.isKinematic = false;
    }

    public void Released()
    {
        isGrabbed = false;
        if (rb != null) rb.isKinematic = true;

        // Recalculate orbit angle based on current position
        Vector3 offset = transform.position - sun.position;
        currentOrbitAngle = Mathf.Atan2(offset.z, offset.x) * Mathf.Rad2Deg;
    }
}
