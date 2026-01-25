using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0f, 12f, -100f);
    public float followSpeed = 1f;
    Vector3 velocity;

    bool locked;

    void LateUpdate()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
            locked = !locked;

        if (target == null || locked) return;

        Vector3 desiredPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(
        transform.position,
        desiredPos,
        ref velocity,
        0.15f);

        transform.LookAt(target.position);
    }
}
