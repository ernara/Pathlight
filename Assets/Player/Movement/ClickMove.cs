using UnityEngine;
using UnityEngine.InputSystem;

public class ClickMove : MonoBehaviour
{
    public float speed = 5f;

    Vector3 target;
    bool hasTarget;
    Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                target = hit.point;
                hasTarget = true;
                if (animator != null)
                    animator.SetBool("IsMoving", true);
            }
        }

        if (hasTarget)
        {
            Vector3 movePos = new Vector3(target.x, transform.position.y, target.z);
            transform.position = Vector3.MoveTowards(transform.position, movePos, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, movePos) < 0.1f)
            {
                hasTarget = false;
                if (animator != null)
                    animator.SetBool("IsMoving", false);
            }
        }
    }

    public void StopMovement()
    {
        hasTarget = false;
        if (animator != null)
            animator.SetBool("IsMoving", false);
    }
}
