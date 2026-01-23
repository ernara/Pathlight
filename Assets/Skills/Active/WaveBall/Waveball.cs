using UnityEngine;
using UnityEngine.InputSystem;

public class Waveball : MonoBehaviour
{
    public GameObject waveballPrefab;
    public Transform firePoint;
    public float speed = 2f;
    public float cooldown = 0.3f;
    public float duration = 6f;
    public int damage = 1;

    float lastCastTime;

    void Update()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            if (Time.time - lastCastTime < cooldown)
                return;

            lastCastTime = Time.time;

            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (!Physics.Raycast(ray, out RaycastHit hit))
                return;

            Vector3 dir = hit.point - firePoint.position;
            dir.y = 0f;
            dir.Normalize();

            GameObject wb = Instantiate(
                waveballPrefab,
                firePoint.position + dir * 0.5f,
                Quaternion.LookRotation(dir)
            );

            wb.GetComponent<WaveballProjectile>()
              .Initialize(dir, speed, duration, damage);
        }
    }
}
