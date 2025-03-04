using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Transform[] positions; // Array of predefined positions
    public Vector3[] offsets; // Array of offsets for each position
    public float followSpeed = 10f;
    public float rotationSpeed = 5f;
    private int currentIndex = 0;
    public bool enableRotation = true; // Option to toggle rotation

    void FixedUpdate()
    {
        if (target == null)
            return;

        // Use individual offset for current target
        Vector3 desiredPosition = target.position + offsets[currentIndex];
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.fixedDeltaTime);

        // Smooth rotation using Slerp if enabled
        if (enableRotation)
        {
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    public void ChangePosition(int index)
    {
        if (index >= 0 && index < positions.Length)
        {
            target = positions[index];
            currentIndex = index;
        }
    }

    public void NextPosition()
    {
        currentIndex = (currentIndex + 1) % positions.Length;
        target = positions[currentIndex];
    }

    public void PreviousPosition()
    {
        currentIndex = (currentIndex - 1 + positions.Length) % positions.Length;
        target = positions[currentIndex];
    }

    public void SelectPosition0() { ChangePosition(0); }
    public void SelectPosition1() { ChangePosition(1); }
    public void SelectPosition2() { ChangePosition(2); }
    public void SelectPosition3() { ChangePosition(3); }
    public void SelectPosition4() { ChangePosition(4); }
    public void SelectPosition5() { ChangePosition(5); }
    public void SelectPosition6() { ChangePosition(6); }
    public void SelectPosition7() { ChangePosition(7); }
    public void SelectPosition8() { ChangePosition(8); }
    public void SelectPosition9() { ChangePosition(9); }
    public void SelectPosition10() { ChangePosition(10); }
    public void SelectPosition11() { ChangePosition(11); }
}
