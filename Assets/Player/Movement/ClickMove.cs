using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class ClickMove : MonoBehaviour
{
    [Header("Base Speed")]
    public float baseSpeed = 5f;

    float flatBonus;
    float percentBonus;

    public LayerMask groundLayer;

    Vector3 target;
    bool hasTarget;
    Animator animator;
    Vector3 lookDir;
    AimController aim;

    void Start()
    {
        aim = GetComponent<AimController>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer, QueryTriggerInteraction.Ignore))
            {
                target = hit.point;
                hasTarget = true;
                lookDir = target - transform.position;
                lookDir.y = 0f;

                animator?.SetBool("IsMoving", true);
                aim?.SetAim(target - transform.position);
            }
        }

        if (hasTarget)
        {
            if (lookDir != Vector3.zero)
            {
                Quaternion rot = Quaternion.LookRotation(lookDir);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    rot,
                    10f * Time.deltaTime
                );
            }

            Vector3 movePos = new Vector3(target.x, transform.position.y, target.z);

            transform.position = Vector3.MoveTowards(
                transform.position,
                movePos,
                GetFinalSpeed() * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, movePos) < 0.1f)
            {
                hasTarget = false;
                animator?.SetBool("IsMoving", false);
            }
        }
    }

    float GetFinalSpeed()
    {
        float result = baseSpeed + flatBonus;
        result *= (1f + percentBonus);
        return result;
    }

    public void AddSpeedModifier(float flat, float percent)
    {
        flatBonus += flat;
        percentBonus += percent;
    }

    public void RemoveSpeedModifier(float flat, float percent)
    {
        flatBonus -= flat;
        percentBonus -= percent;
    }

    public void StopMovement()
    {
        hasTarget = false;
        animator?.SetBool("IsMoving", false);
    }
}
