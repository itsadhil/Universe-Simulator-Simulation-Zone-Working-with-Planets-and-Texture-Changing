using UnityEngine;

public class CameraSkyboxSwitcher : MonoBehaviour
{
    public Material defaultSkybox;
    public Material secondSkybox;
    public Material thirdSkybox;

    private Skybox skyboxComponent;

    void Start()
    {
        // Get or add the Skybox component to the camera
        skyboxComponent = GetComponent<Skybox>();
        if (skyboxComponent == null)
        {
            skyboxComponent = gameObject.AddComponent<Skybox>();
        }

        // Set the default skybox at the start
        //SetDefaultSkybox();
    }

    public void SetDefaultSkybox()
    {
        skyboxComponent.material = defaultSkybox;
    }

    public void SetSecondSkybox()
    {
        skyboxComponent.material = secondSkybox;
    }

    public void SetThirdSkybox()
    {
        skyboxComponent.material = thirdSkybox;
    }
}
