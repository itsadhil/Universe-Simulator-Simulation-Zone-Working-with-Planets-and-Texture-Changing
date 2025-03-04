using UnityEngine;

public class DisableWalls : MonoBehaviour
{
    public void DisableWallsFunction()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Walls");
        foreach (GameObject wall in walls)
        {
            wall.SetActive(false);
        }
    }
}
