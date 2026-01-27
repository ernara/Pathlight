using UnityEngine;

public class ExplosionAOE : HitSkillBase
{
    public float lifetime = 0.5f;

    private SpriteRenderer sprite;
    private float timer;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (sprite != null)
        {
            Color c = sprite.color;
            c.a = Mathf.Lerp(1f, 0f, timer / lifetime);
            sprite.color = c;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    TryHit(other);
    //}
}
