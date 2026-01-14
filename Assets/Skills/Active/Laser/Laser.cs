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

        GameObject laser = Instantiate(laserPrefab, firePoint.position, Quaternion.LookRotation(dir));

        //Renderer r = laser.GetComponent<Renderer>();
        //if (r != null)
        //{
        //    r.material = new Material(r.material);
        //    r.material.color = GetRandomLaserColor();
        //}

        laser.transform.localScale = new Vector3(
            laser.transform.localScale.x,
            laser.transform.localScale.y,
            laserLength
        );

        laser.transform.position += dir * laserLength * 0.5f;

        Destroy(laser, laserDuration);
    }
}
