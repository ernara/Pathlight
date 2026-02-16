using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int maxHits = 4;
    public bool isAlive { get; private set; } = true;

    protected int hits;

    void Awake()
    {
        hits = 0;
        isAlive = true;
    }

    public void ApplyDamage(int amount)
    {
        if (!isAlive)
            return; 

        hits += amount;

        if (hits >= maxHits)
            Die();
    }

    protected virtual void Die()
    {
        isAlive = false;

        Respawner respawner = GetComponent<Respawner>();
        if (respawner != null)
        {
            respawner.OnDeath(); 
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    public void ResetHits()
    {
        hits = 0;
        isAlive = true;
    }
}
