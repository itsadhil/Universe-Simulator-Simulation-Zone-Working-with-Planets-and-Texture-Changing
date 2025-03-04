using UnityEngine;

public class OrbitingOObjects : MonoBehaviour
{
    public Transform centerObject;
    public Transform[] orbitingObjects;
    public float orbitSpeed = 10f;
    private bool[] isOrbiting;

    void Start()
    {
        if (orbitingObjects.Length != 9)
        {
            Debug.LogError("There must be exactly 9 orbiting objects.");
            return;
        }
        isOrbiting = new bool[9];
        for (int i = 0; i < 9; i++)
        {
            isOrbiting[i] = true;
        }
    }

    void Update()
    {
        for (int i = 0; i < 9; i++)
        {
            if (isOrbiting[i] && orbitingObjects[i] != null)
            {
                orbitingObjects[i].RotateAround(centerObject.position, Vector3.up, orbitSpeed * Time.deltaTime);
            }
        }
    }

    public void StopOrbiting1() { isOrbiting[0] = false; }
    public void StopOrbiting2() { isOrbiting[1] = false; }
    public void StopOrbiting3() { isOrbiting[2] = false; }
    public void StopOrbiting4() { isOrbiting[3] = false; }
    public void StopOrbiting5() { isOrbiting[4] = false; }
    public void StopOrbiting6() { isOrbiting[5] = false; }
    public void StopOrbiting7() { isOrbiting[6] = false; }
    public void StopOrbiting8() { isOrbiting[7] = false; }
    public void StopOrbiting9() { isOrbiting[8] = false; }

    public void StartOrbiting1() { isOrbiting[0] = true; }
    public void StartOrbiting2() { isOrbiting[1] = true; }
    public void StartOrbiting3() { isOrbiting[2] = true; }
    public void StartOrbiting4() { isOrbiting[3] = true; }
    public void StartOrbiting5() { isOrbiting[4] = true; }
    public void StartOrbiting6() { isOrbiting[5] = true; }
    public void StartOrbiting7() { isOrbiting[6] = true; }
    public void StartOrbiting8() { isOrbiting[7] = true; }
    public void StartOrbiting9() { isOrbiting[8] = true; }
}