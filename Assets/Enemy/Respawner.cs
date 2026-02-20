using UnityEngine;

public class Respawner : MonoBehaviour
{
    public float respawnTime = 3f;         
    public Vector3 spawnOffset = Vector3.zero; 
    public GameObject visual;              

    private Vector3 spawnPosition;

    void Awake()
    {
        spawnPosition = transform.position;
        if (visual == null && transform.childCount > 0)
            visual = transform.GetChild(0).gameObject;
    }

    public void OnDeath()
    {
        if (visual != null) visual.SetActive(false);

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        Invoke(nameof(Respawn), respawnTime);
    }

    void Respawn()
    {
        transform.position = spawnPosition + spawnOffset;

        if (visual != null) visual.SetActive(true);
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;

        EnemyBase enemy = GetComponent<EnemyBase>();
        if (enemy != null)
            enemy.ResetHits();
    }

}
