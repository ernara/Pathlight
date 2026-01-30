using UnityEngine;

public class Enemy2SpawnerAroundPlayer : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int spawnCount = 5;
    public float spawnRadius = 10f;

    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
            Instantiate(enemyPrefab, transform.position + (Vector3)(Random.insideUnitCircle * spawnRadius), Quaternion.identity);
    }
}
