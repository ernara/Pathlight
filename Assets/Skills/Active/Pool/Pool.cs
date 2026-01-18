using UnityEngine;
using UnityEngine.InputSystem;

public class Pool : MonoBehaviour
{
    public GameObject poolPrefab;
    public float lifetime = 5f;

    void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            Debug.Log("hmmmm");
            CastPool();
        }
    }

    void CastPool()
    {
        if (Camera.main == null)
        {
            Debug.LogError("No Main Camera");
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (!Physics.Raycast(ray, out RaycastHit hit, 500f))
        {
            Debug.Log("Raycast failed");
            return;
        }

        Debug.Log("Hit: " + hit.collider.name);

        Vector3 spawnPos = hit.point;
        spawnPos.y += 0.02f; // avoid z-fighting

        GameObject pool = Instantiate(
            poolPrefab,
            spawnPos,
            Quaternion.identity
        );

        Destroy(pool, lifetime);
    }
}
