using UnityEngine;

public class HoldObjectPosition : MonoBehaviour
{
    public Vector3 targetPosition; // Set this in the Inspector or via script

    void Update()
    {
        transform.position = targetPosition; // Keep object at the given position
    }
}
