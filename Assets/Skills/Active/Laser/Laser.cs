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
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit))
            return;

        Vector3 dir = hit.point - firePoint.position;
        dir.y = 0f;
        dir.Normalize();

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
