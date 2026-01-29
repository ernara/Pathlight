using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 1;

    public void TakeHit(int damage)
    {
        hp -= damage;

        if (hp <= 0)
            Die();
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

