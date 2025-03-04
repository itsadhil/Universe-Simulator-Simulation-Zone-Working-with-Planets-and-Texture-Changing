using UnityEngine;

public class DiagonalOrbit : MonoBehaviour
{
    public Transform centerObject;   // The object to orbit around (e.g., the Sun)
    public float orbitSpeed = 10f;   // Speed of orbiting around the center object
    public float selfRotationSpeed = 50f; // Speed of the object's own rotation
    public Vector3 orbitAxis = new Vector3(1, 1, 0); // Diagonal axis for orbiting
    public Vector3 selfRotationAxis = Vector3.up; // Axis for the object's own rotation

    void Start()
    {
        // Normalize the orbit axis to ensure consistent orbiting speed
        orbitAxis = orbitAxis.normalized;
    }

    void Update()
    {
        if (centerObject != null)
        {
            // Orbit around the center object along the specified diagonal axis
            transform.RotateAround(centerObject.position, orbitAxis, orbitSpeed * Time.deltaTime);

            // Rotate around its own axis
            transform.Rotate(selfRotationAxis, selfRotationSpeed * Time.deltaTime);
        }
    }
}
