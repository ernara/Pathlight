using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;


public class Fireball : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireballSpeed = 10f;
    public float spreadAngle = 10f;
    public float cooldown = 0.1f;
    public float projectileDuration = 4f;
    public TextMeshProUGUI cooldownText;



    bool lastHandRight = false;
    float lastCastTime = -999f;

    public event System.Action OnFireballCast;

    public float CooldownRemaining =>
        Mathf.Max(0f, GetFinalCooldown() - (Time.time - lastCastTime));

    public float CooldownTotal =>
        GetFinalCooldown();

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
        float remaining = GetFinalCooldown() - (Time.time - lastCastTime);

        if (remaining > 0)
            cooldownText.text = remaining.ToString("F1");
        else
            cooldownText.text = "";


        if (!Keyboard.current.qKey.wasPressedThisFrame)
            return;

        if (Time.time - lastCastTime < GetFinalCooldown())
            return;

        lastCastTime = Time.time;
        OnFireballCast?.Invoke();

        Animator animator = GetComponentInChildren<Animator>();
        if (animator != null)
        {
            animator.SetTrigger(lastHandRight ? "PunchLeft" : "PunchRight");
            lastHandRight = !lastHandRight;
        }

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!Physics.Raycast(ray, out RaycastHit hit))
            return;

        Vector3 baseDir = hit.point - firePoint.position;
        baseDir.y = 0f;
        baseDir.Normalize();

        AimController aim = GetComponent<AimController>();
        if (aim != null)
            aim.SetAim(baseDir);

        int total = GetProjectileCount();

        for (int i = 0; i < total; i++)
        {
            float angleOffset = 0f;
            if (total > 1)
                angleOffset = Mathf.Lerp(-spreadAngle, spreadAngle, i / (float)(total - 1));

            Vector3 dir = Quaternion.Euler(0f, angleOffset, 0f) * baseDir;
            Vector3 spawnPos = firePoint.position + dir * 0.5f;

            GameObject fb = Instantiate(
                fireballPrefab,
                spawnPos,
                Quaternion.LookRotation(dir)
            );

            fb.GetComponent<FireballProjectile>()
              .Initialize(dir, fireballSpeed, projectileDuration, gameObject);

            SpellEcho echo = GetComponent<SpellEcho>();
            if (echo != null)
                StartCoroutine(EchoFireball(spawnPos, dir, echo.echoDelay));
        }
    }

    IEnumerator EchoFireball(Vector3 origin, Vector3 dir, float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject fb = Instantiate(
            fireballPrefab,
            origin + dir * 0.5f,
            Quaternion.LookRotation(dir)
        );

        fb.GetComponent<FireballProjectile>()
          .Initialize(dir, fireballSpeed, projectileDuration, gameObject);
    }
}
