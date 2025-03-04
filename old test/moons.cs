using UnityEngine;

public class MoonOrbit : MonoBehaviour
{
    public Transform planet;          // The planet around which the moon orbits
    public float orbitSpeed = 10f;    // Speed of the moon's orbit
    public Vector3 orbitAxis = new Vector3(1, 1, 0); // Axis for orbiting (e.g., diagonal)

    private Vector3 initialOffset;    // Initial offset from the planet

    void Start()
    {
        if (planet != null)
        {
            // Calculate and store the initial offset
            initialOffset = transform.position - planet.position;
        }
        else
        {
            Debug.LogError("Planet (center object) is not assigned.");
        }
    }

    void Update()
    {
        if (planet != null)
        {
            // Rotate the moon around the planet along the specified axis
            transform.RotateAround(planet.position, orbitAxis.normalized, orbitSpeed * Time.deltaTime);

            // Maintain the fixed distance by resetting the position based on the initial offset's magnitude
            Vector3 currentOffset = transform.position - planet.position;
            transform.position = planet.position + currentOffset.normalized * initialOffset.magnitude;
        }
    }
}
