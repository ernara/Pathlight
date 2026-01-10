using UnityEngine;
using UnityEngine.InputSystem;

public class Dash : MonoBehaviour
{
    public float dashDistance = 5f;

    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 dir = (hit.point - transform.position).normalized;
                transform.position += new Vector3(dir.x, 0, dir.z) * dashDistance;
            }
        }
    }
}
