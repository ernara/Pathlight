using UnityEngine;
using System.Collections.Generic;

public class Boss : EnemyBase
{
    Dictionary<Spell, float> dotTimers = new Dictionary<Spell, float>();

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

    void Update()
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
                Vector3 poolPos = spell.transform.position;
                Vector3 bossPos = transform.position;

                Vector2 pool2D = new Vector2(poolPos.x, poolPos.z);
                Vector2 boss2D = new Vector2(bossPos.x, bossPos.z);

                float dist = Vector2.Distance(pool2D, boss2D);

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
}
