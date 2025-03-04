using UnityEngine;

public class RotateToCamera : MonoBehaviour
{
    private Transform target;

    void Start()
    {
        GameObject cameraObject = GameObject.FindGameObjectWithTag("SkyboxCamera");
        if (cameraObject != null)
        {
            target = cameraObject.transform;
        }
        else
        {
            Debug.LogError("No object with tag 'Camera' found!");
        }
    }

    void Update()
    {
        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2f);
        }
    }
}
