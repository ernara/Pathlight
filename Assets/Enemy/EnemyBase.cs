using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int maxHits = 4;
    public bool isAlive { get; private set; } = true;

    protected int hits;

    Dictionary<Spell, float> dotTimers = new Dictionary<Spell, float>();

    protected virtual void Awake()
    {
        hits = 0;
        isAlive = true;
    }

    protected virtual void Update()
    {
        HandleDamageOverTime();
    }

    void OnCollisionEnter(Collision col)
    {
        HandleInstantSpell(col.gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        HandleInstantSpell(col.gameObject);

        Spell spell = col.GetComponent<Spell>();
        if (spell != null && spell.damageOverTime)
        {
            dotTimers[spell] = 0f;
        }
    }

    void OnTriggerExit(Collider col)
    {
        Spell spell = col.GetComponent<Spell>();
        if (spell != null && dotTimers.ContainsKey(spell))
        {
            dotTimers.Remove(spell);
        }
    }

    void HandleDamageOverTime()
    {
        List<Spell> spells = new List<Spell>(dotTimers.Keys);

        foreach (Spell spell in spells)
        {
            if (spell == null)
            {
                dotTimers.Remove(spell);
                continue;
            }

            dotTimers[spell] += Time.deltaTime;

            if (dotTimers[spell] >= spell.tickInterval)
            {
                float dist = Vector3.Distance(spell.transform.position, transform.position);

                if (dist <= spell.radius)
                {
                    ApplyDamage(spell.damage);
                }

                dotTimers[spell] = 0f;
            }
        }
    }

    void HandleInstantSpell(GameObject obj)
    {
        Spell spell = obj.GetComponent<Spell>();
        if (spell == null) return;
        if (spell.damageOverTime) return;

        ApplyDamage(spell.damage);
    }

    public void ApplyDamage(int amount)
    {
        if (!isAlive) return;

        hits += amount;

        if (hits >= maxHits)
            Die();
    }

    protected virtual void Die()
    {
        isAlive = false;

        EnemyRespawner respawner = GetComponent<EnemyRespawner>();
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
