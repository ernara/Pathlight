//using UnityEngine;

//public class Enemy : MonoBehaviour
//{
//    public int hp = 1;

//    float lastPoolHitTime = -999f;
//    public float poolHitCooldown = 5f;

//    public void TakeHit(GameObject source)
//    {
//        if (source.CompareTag("Pool"))
//        {
//            if (Time.time - lastPoolHitTime < poolHitCooldown)
//                return;

//            lastPoolHitTime = Time.time;
//        }

//        hp -= 1;

//        if (hp <= 0)
//            Die();
//    }

//    void Die()
//    {
//        Destroy(gameObject);
//    }
//}

using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp = 3;

    public void TakeHit()
    {
        hp -= 1;

        if (hp <= 0)
            Destroy(gameObject);
    }
}
