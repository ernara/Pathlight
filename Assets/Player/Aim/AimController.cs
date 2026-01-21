using UnityEngine;

public class AimController : MonoBehaviour
{
    Vector3 aimDir;
    bool hasAim;

    public void SetAim(Vector3 dir)
    {
        dir.y = 0f;
        if (dir == Vector3.zero) return;
        aimDir = dir.normalized;
        hasAim = true;
    }

    void Update()
    {
        if (!hasAim) return;

        Quaternion rot = Quaternion.LookRotation(aimDir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 12f * Time.deltaTime);
    }
}
