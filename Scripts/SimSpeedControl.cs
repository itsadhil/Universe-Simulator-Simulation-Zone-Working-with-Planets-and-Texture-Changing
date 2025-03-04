using UnityEngine;

public class TimeScaleController : MonoBehaviour
{
    private readonly float defaultTimeScale = 0.4f;
    private float timeStep = 0.3f;
    private float storedTimeScale;
    public float transitionSpeed = 2f;
    private bool isPaused = false;
    private float lastTimeScale;

    void Start()
    {
        storedTimeScale = defaultTimeScale;
        lastTimeScale = storedTimeScale;
    }

    void Update()
    {
        float targetTimeScale = isPaused ? 0 : lastTimeScale;
        Time.timeScale = Mathf.Lerp(Time.timeScale, targetTimeScale, Time.unscaledDeltaTime * transitionSpeed);
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    public void TogglePausePlay()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            lastTimeScale = Time.timeScale;
        }
    }

    public void IncreaseTimeScale()
    {
        lastTimeScale += timeStep;
    }

    public void DecreaseTimeScale()
    {
        lastTimeScale = Mathf.Max(0.1f, lastTimeScale - timeStep);
    }
}
