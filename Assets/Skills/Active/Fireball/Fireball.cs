using UnityEngine;
using UnityEngine.InputSystem;

public class Fireball : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireballSpeed = 20f;
    public float spreadAngle = 10f; 
    public float cooldown = 1f;
    public float projectileDuration = 2f;

    float lastCastTime;

    int GetProjectileCount()
    {
        int count = 1;
        MoreProjectiles support = GetComponent<MoreProjectiles>();
        if (support != null)
            count += support.extraProjectiles;
        return count;
    }

    float GetFinalCooldown()
    {
        float cd = cooldown;
        CooldownReduction cdr = GetComponent<CooldownReduction>();
        if (cdr != null)
            cd *= 1f - cdr.reductionPercent;
        return Mathf.Max(0.05f, cd);
    }

    void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            if (Time.time - lastCastTime < GetFinalCooldown())
                return;

            lastCastTime = Time.time;
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 baseDir = hit.point - firePoint.position;
                baseDir.y = 0f;
                baseDir.Normalize();

                int total = GetProjectileCount();
                for (int i = 0; i < total; i++)
                {
                    float angleOffset = 0f;
                    if (total > 1)
                        angleOffset = Mathf.Lerp(-spreadAngle, spreadAngle, i / (float)(total - 1));

                    Vector3 dir = Quaternion.Euler(0f, angleOffset, 0f) * baseDir;

                    Vector3 spawnPos = firePoint.position;
                    spawnPos.y = firePoint.position.y;

                    GameObject fb = Instantiate(fireballPrefab, spawnPos, Quaternion.LookRotation(dir));

                    Collider playerCol = GetComponent<Collider>();
                    if (playerCol != null)
                        Physics.IgnoreCollision(fb.GetComponent<Collider>(), playerCol);

                    fb.AddComponent<FireballProjectile>()
                        .Initialize(dir, fireballSpeed, projectileDuration, gameObject);
                }
            }
        }
    }
}

public class FireballProjectile : MonoBehaviour
{
    Vector3 direction;
    float speed;
    float lifetime;

    public void Initialize(Vector3 dir, float spd, float baseDuration, GameObject caster)
    {
        direction = dir;
        speed = spd;
        lifetime = baseDuration;

        MoreDuration md = caster.GetComponent<MoreDuration>();
        if (md != null)
            lifetime *= md.durationMultiplier;

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        Destroy(gameObject);
    }
}

