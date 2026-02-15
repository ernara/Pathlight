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

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (!groundPlane.Raycast(ray, out float distance))
        {
            Debug.Log("Plane raycast failed");
            return;
        }

        Vector3 spawnPos = ray.GetPoint(distance);
        spawnPos.y = 0.01f;

        GameObject pool = Instantiate(poolPrefab, spawnPos, Quaternion.identity);

        Destroy(pool, lifetime);
    }

}


