using UnityEngine;

public class ClonePlayer : MonoBehaviour
{
    public GameObject clonePrefab;
    public int cloneCount = 10;
    public float spawnRadius = 5f;

    void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.rKey.wasPressedThisFrame)
        {
            Debug.Log("SPAWNING TEST CLONES");
            SpawnClones();
        }
    }

    void SpawnClones()
    {
        if (clonePrefab == null)
        {
            Debug.LogError("CLONE PREFAB IS NULL!");
            return;
        }

        for (int i = 0; i < cloneCount; i++)
        {
            Vector2 offset = Random.insideUnitCircle * spawnRadius;

            Vector3 pos = transform.position + new Vector3(offset.x, 0, offset.y);

            Instantiate(clonePrefab, pos, Quaternion.identity);
        }

        Debug.Log("CLONES SPAWNED SUCCESSFULLY");
    }
}
