using UnityEngine;
using UnityEngine.InputSystem;

public class CubeSpell : MonoBehaviour
{
    public GameObject cubePrefab;
    public Transform firePoint;
    public float speed = 10f;
    public float cooldown = 1f;

    float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer < cooldown) return;
        timer = 0f;

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;

        Vector3 dir = hit.point - firePoint.position;
        dir.y = 0f;

        Rigidbody rb = Instantiate(
            cubePrefab,
            firePoint.position,
            Quaternion.LookRotation(dir)
        ).GetComponent<Rigidbody>();

        rb.linearVelocity = dir.normalized * speed;

        Destroy(rb.gameObject, 4f);
    }
}
