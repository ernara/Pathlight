using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    int hits = 0;

    public void Hit()
    {
        hits++;
        Debug.Log("Enemy2 hit: " + hits);

        if (hits >= 3)
        {
            Debug.Log("Enemy2 destroyed");
            Destroy(gameObject);
        }
    }
}
