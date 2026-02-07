using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Dash : MonoBehaviour
{
    public float dashDistance = 5f;

    public float cooldown = 3f;
    float lastDashTime = -999f;

    public TextMeshProUGUI dashText;

    public delegate void DashUsed();
    public event DashUsed OnDashUsed;

    [HideInInspector] public bool DashTriggered = false;

    void Update()
    {
        DashTriggered = false;

        float remaining = cooldown - (Time.time - lastDashTime);

        // UI
        if (dashText != null)
        {
            if (remaining <= 0)
                dashText.text = "Dash - READY";
            else
                dashText.text = "Dash - " + remaining.ToString("F1") + "s";
        }

        // Cooldown check
        if (Keyboard.current.rKey.wasPressedThisFrame && remaining <= 0)
        {
            lastDashTime = Time.time;

            PerformDash();
            DashTriggered = true;
            OnDashUsed?.Invoke();
        }
    }

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
