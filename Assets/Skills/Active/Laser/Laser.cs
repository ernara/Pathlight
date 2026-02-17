using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Laser : HitSkillBase
{
    public GameObject laserPrefab;
    public Transform firePoint;
    public float laserDuration = 0.5f;
    public float laserLength = 100f;

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            FireLaser();
        }
    }

    void FireLaser()
    {
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        // Raycast against ground only
        if (!Physics.Raycast(ray, out RaycastHit hit, 1000f))
            return;

        // Keep target Y at the same height as firePoint (ignore enemy collider height)
        Vector3 target = hit.point;
        target.y = firePoint.position.y;

        Vector3 dir = (target - firePoint.position).normalized;

        // damage anything in the path
        Ray shootRay = new Ray(firePoint.position, dir);
        if (Physics.Raycast(shootRay, out RaycastHit laserHit, laserLength))
        {
            TryHit(laserHit.collider);
        }

        SpawnLaser(firePoint.position, dir);

        SpellEcho echo = GetComponent<SpellEcho>();
        if (echo != null)
        {
            StartCoroutine(EchoLaser(firePoint.position, dir, echo.echoDelay));
        }
    }

    IEnumerator EchoLaser(Vector3 origin, Vector3 dir, float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnLaser(origin, dir);
    }

    void SpawnLaser(Vector3 origin, Vector3 dir)
    {
        GameObject laser = Instantiate(laserPrefab, origin, Quaternion.LookRotation(dir));

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
        if (thicc == null) return;

        laser.transform.localScale = new Vector3(
            laser.transform.localScale.x * thicc.thicknessMultiplier,
            laser.transform.localScale.y * thicc.thicknessMultiplier,
            laser.transform.localScale.z
        );
    }
}
