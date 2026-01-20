using UnityEngine;

public abstract class HitSkillBase : MonoBehaviour
{
    public int damagePerHit = 1;

    protected void TryHit(Collider col)
    {
        Enemy enemy = col.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeHit(damagePerHit);
        }
    }
}
