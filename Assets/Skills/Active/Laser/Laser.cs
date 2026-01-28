using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Laser : HitSkillBase
{
    public GameObject laserPrefab;
    public Transform firePoint;
    public float laserDuration = 0.5f;
    public float laserLength = 100f;
    public float targetRange = 25f;

    [Header("Targeting")]
    public LayerMask enemyLayer;

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            FireLaser();
        }
    }

    void FireLaser()
    {
        Collider[] enemies = Physics.OverlapSphere(
            firePoint.position,
            targetRange,
            enemyLayer
        );

        if (enemies.Length == 0)
        {
            Debug.Log("No enemies in range");
            return;
        }

        // pick random enemy
        Collider target = enemies[Random.Range(0, enemies.Length)];
        Debug.Log("Targeting: " + target.name);

        Vector3 dir = target.bounds.center - firePoint.position;
        dir.y = 0f;
        dir.Normalize();

        // damage
        Ray ray = new Ray(firePoint.position, dir);
        if (Physics.Raycast(ray, out RaycastHit hit, laserLength, enemyLayer))
        {
            Debug.Log("Laser hit: " + hit.collider.name);
            TryHit(hit.collider);
        }

        SpawnLaser(firePoint.position, dir);

        SpellEcho echo = GetComponent<SpellEcho>();
        if (echo != null)
        {
            StartCoroutine(EchoLaser(
                firePoint.position,
                dir,
                echo.echoDelay
            ));
        }
    }

    IEnumerator EchoLaser(Vector3 origin, Vector3 dir, float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnLaser(origin, dir);
    }

    void SpawnLaser(Vector3 origin, Vector3 dir)
    {
        GameObject laser = Instantiate(
            laserPrefab,
            origin,
            Quaternion.LookRotation(dir)
        );

        ApplyThicc(laser);

        laser.transform.localScale = new Vector3(
            laser.transform.localScale.x,
            laser.transform.localScale.y,
            laserLength
        );

        laser.transform.position += dir * laserLength * 0.5f;

        Destroy(laser, laserDuration);
    }

    void ApplyThicc(GameObject laser)
    {
        Thicc thicc = GetComponent<Thicc>();
        if (thicc == null)
            return;

        laser.transform.localScale = new Vector3(
            laser.transform.localScale.x * thicc.thicknessMultiplier,
            laser.transform.localScale.y * thicc.thicknessMultiplier,
            laser.transform.localScale.z
        );
    }
}
