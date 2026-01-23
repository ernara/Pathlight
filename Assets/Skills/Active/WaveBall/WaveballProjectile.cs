using UnityEngine;

public class WaveballProjectile : HitSkillBase
{
    Vector3 direction;
    float speed;
    float lifetime;
    int damage;

    public void Initialize(Vector3 dir, float spd, float life, int dmg)
    {
        direction = dir;
        speed = spd;
        lifetime = life;
        damage = dmg;

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            return;

        TryHit(other);
    }
}
