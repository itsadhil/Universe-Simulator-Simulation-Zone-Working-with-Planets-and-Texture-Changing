using UnityEngine;

public class MimicObject : MonoBehaviour
{
    public Transform target; // Assign the target object in the Inspector
    public bool mimicRotation = true; // Toggle for rotation mimicry

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position;
            if (mimicRotation)
            {
                transform.rotation = target.rotation;
            }
        }
    }
}
