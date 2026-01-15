using UnityEngine;
using UnityEngine.InputSystem;

public class Laser : MonoBehaviour
{
    public GameObject laserPrefab;
    public Transform firePoint;
    public float laserDuration = 0.5f;
    public float laserLength = 100f;


    Color GetRandomLaserColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }

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

        Vector3 originPos = firePoint.position;
        Vector3 fireDir = dir;


        GameObject laser = Instantiate(laserPrefab, firePoint.position, Quaternion.LookRotation(dir));

        //uzkomentuoti

        Renderer r = laser.GetComponent<Renderer>();
        if (r != null)
        {
            r.material = new Material(r.material);
            r.material.color = GetRandomLaserColor();
        }

        //

        laser.transform.localScale = new Vector3(
            laser.transform.localScale.x,
            laser.transform.localScale.y,
            laserLength
        );

        laser.transform.position += dir * laserLength * 0.5f;

        SpellEcho echo = GetComponent<SpellEcho>();
        if (echo != null)
        {
            StartCoroutine(EchoLaser(originPos, fireDir, echo.echoDelay));
        }


        Destroy(laser, laserDuration);
    }

    System.Collections.IEnumerator EchoLaser(Vector3 origin, Vector3 dir, float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject echoLaser = Instantiate(
            laserPrefab,
            origin,
            Quaternion.LookRotation(dir)
        );

        echoLaser.transform.localScale = new Vector3(
            echoLaser.transform.localScale.x,
            echoLaser.transform.localScale.y,
            laserLength
        );

        echoLaser.transform.position += dir * laserLength * 0.5f;

        Destroy(echoLaser, laserDuration);
    }

}
