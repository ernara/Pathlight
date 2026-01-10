using UnityEngine;
using UnityEngine.InputSystem;

public class Fireball : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireballSpeed = 10f;

    void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 dir = (hit.point - firePoint.position).normalized;
                GameObject fb = Instantiate(fireballPrefab, firePoint.position, Quaternion.LookRotation(dir));
                fb.GetComponent<Rigidbody>().linearVelocity = dir * fireballSpeed;
            }
        }
    }
}
