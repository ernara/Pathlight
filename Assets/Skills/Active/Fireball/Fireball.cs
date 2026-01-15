using UnityEngine;
using UnityEngine.InputSystem;

public class Fireball : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireballSpeed = 10f;
    public float spreadAngle = 10f; 
    public float cooldown = 0.1f;
    public float projectileDuration = 4f;
    bool lastHandRight = false;


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

            Animator animator = GetComponentInChildren<Animator>();
            if (animator != null)
            {
                if (lastHandRight)
                    animator.SetTrigger("PunchLeft");
                else
                    animator.SetTrigger("PunchRight");

                lastHandRight = !lastHandRight; 
            }

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

                    SpellEcho echo = GetComponent<SpellEcho>();
                    if (echo != null)
                    {
                        StartCoroutine(EchoFireball(
                            spawnPos,
                            dir,
                            echo.echoDelay
                        ));
                    }


                    Collider playerCol = GetComponent<Collider>();
                    if (playerCol != null)
                        Physics.IgnoreCollision(fb.GetComponent<Collider>(), playerCol);

                    fb.AddComponent<FireballProjectile>()
                        .Initialize(dir, fireballSpeed, projectileDuration, gameObject);
                }
            }


        }
    }

    System.Collections.IEnumerator EchoFireball(Vector3 origin, Vector3 dir, float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject fb = Instantiate(
            fireballPrefab,
            origin,
            Quaternion.LookRotation(dir)
        );

        fb.AddComponent<FireballProjectile>()
          .Initialize(dir, fireballSpeed, projectileDuration, gameObject);
    }

}

public class FireballProjectile : MonoBehaviour
{
    Vector3 direction;
    float speed;
    float lifetime;
    Vector3 startPosition;
    bool returning = false;

    public void Initialize(Vector3 dir, float spd, float baseDuration, GameObject caster)
    {
        direction = dir;
        speed = spd;
        lifetime = baseDuration;
        startPosition = transform.position;

        MoreDuration md = caster.GetComponent<MoreDuration>();
        if (md != null)
            lifetime *= md.durationMultiplier;

        ReturnToStart rts = caster.GetComponent<ReturnToStart>();
        if (rts != null && rts.enableReturn)
            Invoke(nameof(StartReturn), lifetime);
        else
            Destroy(gameObject, lifetime);
    }

    void StartReturn()
    {
        returning = true;
        direction = (startPosition - transform.position).normalized;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (returning && Vector3.Distance(transform.position, startPosition) < 0.1f)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        Destroy(gameObject);
    }
}



