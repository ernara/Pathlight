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
            CastPool();
        }
    }

    void CastPool()
    {
        if (Camera.main == null)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (!groundPlane.Raycast(ray, out float distance))
        {
            return;
        }

        Vector3 spawnPos = ray.GetPoint(distance);
        spawnPos.y = 0.01f;

        GameObject pool = Instantiate(poolPrefab, spawnPos, Quaternion.identity);

        Destroy(pool, lifetime);
    }

}


