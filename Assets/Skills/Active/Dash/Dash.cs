using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    public float dashDistance = 5f;

    // Event for support skills
    public delegate void DashUsed();
    public event DashUsed OnDashUsed;

    // Flags for clone mimic
    [HideInInspector] public bool DashTriggered = false;

    void Update()
    {
        DashTriggered = false;

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            PerformDash();
            DashTriggered = true;

            OnDashUsed?.Invoke(); // notify support skills like ClonePlayer
        }
    }

    // Public method so clones can dash too
    public void PerformDash()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 dir = (hit.point - transform.position).normalized;
            transform.position += new Vector3(dir.x, 0, dir.z) * dashDistance;
            GetComponent<ClickMove>().StopMovement();
        }
    }
}
