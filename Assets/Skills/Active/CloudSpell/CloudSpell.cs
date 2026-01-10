using UnityEngine;
using UnityEngine.InputSystem;

public class CloudSpell : MonoBehaviour
{
    public GameObject cloudPrefab;   // prefab of one cloud
    public Transform spawnPoint;     // where clouds appear
    public float cloudSpeed = 5f;
    public int cloudCount = 5;       // number of clouds per cast
    public float spreadAngle = 15f;  // degrees of spread

    void Update()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 dir = (hit.point - spawnPoint.position).normalized;

                for (int i = 0; i < cloudCount; i++)
                {
                    // random spread
                    float angle = Random.Range(-spreadAngle, spreadAngle);
                    Vector3 spreadDir = Quaternion.Euler(0, angle, 0) * dir;

                    GameObject cloud = Instantiate(cloudPrefab, spawnPoint.position, Quaternion.LookRotation(spreadDir));
                    Rigidbody rb = cloud.GetComponent<Rigidbody>();
                    if (rb != null)
                        rb.linearVelocity = spreadDir * cloudSpeed;
                }
            }
        }
    }
}
