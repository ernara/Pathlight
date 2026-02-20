using UnityEngine;

public class EnemySpawner2 : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float spawnRadiusMin = 6f;
    public float spawnRadiusMax = 10f;

    public float spawnInterval = 2f;
    float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Vector2 circle = Random.insideUnitCircle.normalized *
                         Random.Range(spawnRadiusMin, spawnRadiusMax);

        Vector3 pos = transform.position + new Vector3(circle.x, 0f, circle.y);

        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}


