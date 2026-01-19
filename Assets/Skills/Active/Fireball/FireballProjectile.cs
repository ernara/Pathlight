using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    Vector3 direction;
    float speed;
    float lifetime;
    Vector3 startPosition;
    bool returning = false;

    public void Initialize(Vector3 dir, float spd, float baseDuration, GameObject caster)
    {
        direction = dir;
        speed = spd;
        lifetime = baseDuration;
        startPosition = transform.position;

        MoreDuration md = caster.GetComponent<MoreDuration>();
        if (md != null)
            lifetime *= md.durationMultiplier;

        ReturnToStart rts = caster.GetComponent<ReturnToStart>();
        if (rts != null && rts.enableReturn)
            Invoke(nameof(StartReturn), lifetime);
        else
            Destroy(gameObject, lifetime);
    }

    void StartReturn()
    {
        returning = true;
        direction = (startPosition - transform.position).normalized;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (returning && Vector3.Distance(transform.position, startPosition) < 0.1f)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeHit(gameObject);
            Destroy(gameObject);
            return;
        }

        if (other.CompareTag("Player")) return;
        Destroy(gameObject);
    }
}
