using UnityEngine;

public class Boss : MonoBehaviour
{
    int hits;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            other.tag = "Untagged";      
            hits++;
            if (hits >= 4) Destroy(gameObject);
        }
    }
}
