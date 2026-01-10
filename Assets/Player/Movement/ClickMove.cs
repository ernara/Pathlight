using UnityEngine;
using UnityEngine.InputSystem;

public class ClickMove : MonoBehaviour
{
    public float speed = 5f;  // player speed

    Vector3 target;
    bool hasTarget;

    void Update()
    {
        // Check left mouse click
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                target = hit.point;
                hasTarget = true;
            }
        }

        // Move player toward target
        if (hasTarget)
        {
            Vector3 movePos = new Vector3(target.x, transform.position.y, target.z);
            transform.position = Vector3.MoveTowards(transform.position, movePos, speed * Time.deltaTime);

            // Stop moving if reached target
            if (Vector3.Distance(transform.position, movePos) < 0.1f)
                hasTarget = false;
        }
    }
}
