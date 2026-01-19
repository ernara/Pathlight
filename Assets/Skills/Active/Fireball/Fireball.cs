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

                    //change1/3
                    //Vector3 spawnPos = firePoint.position;
                    //spawnPos.y = firePoint.position.y;

                    Vector3 spawnPos = firePoint.position + dir * 0.5f;
                    /////

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

                    //change2/3
                    //Collider playerCol = GetComponent<Collider>();
                    //if (playerCol != null)
                    //    Physics.IgnoreCollision(fb.GetComponent<Collider>(), playerCol);
                    /////
                    ///
                    fb.GetComponent<FireballProjectile>()
  .Initialize(dir, fireballSpeed, projectileDuration, gameObject);
                }
            }


        }
    }

    System.Collections.IEnumerator EchoFireball(Vector3 origin, Vector3 dir, float delay)
    {
        yield return new WaitForSeconds(delay);
        //change3/3
        //GameObject fb = Instantiate(
        //    fireballPrefab,
        //    origin,
        //    Quaternion.LookRotation(dir)
        //);

        GameObject fb = Instantiate(
        fireballPrefab,
        origin + dir * 0.5f,
        Quaternion.LookRotation(dir)
);

        ///

        ///4
        fb.GetComponent<FireballProjectile>()
          .Initialize(dir, fireballSpeed, projectileDuration, gameObject);
    }

}




