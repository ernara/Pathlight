using UnityEngine;
using UnityEngine.InputSystem;

public class IceColdPulse : MonoBehaviour
{
    public float range = 10f;
    public LayerMask enemyLayer;

    public GameObject pulsePrefab;
    public float pulseDuration = 0.5f;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.cKey.wasPressedThisFrame)
        {
            Debug.Log("C pressed");
            Cast();
        }
    }

    void Cast()
    {
        if (pulsePrefab != null)
        {
            GameObject pulse = Instantiate(pulsePrefab, transform.position, Quaternion.identity);
            pulse.transform.localScale = Vector3.one * range * 2f;
            Destroy(pulse, pulseDuration);
        }


        Collider[] hits = Physics.OverlapSphere(transform.position, range);


        foreach (var h in hits)
        {
            Enemy2 e = h.GetComponent<Enemy2>();
            if (e != null)
            {
                e.Hit();
            }
        }

        if (hits.Length == 0)
            return;

        Enemy2 enemy2 = hits[0].GetComponent<Enemy2>();
        if (enemy2 != null)
        {
            enemy2.Hit();
        }
    }

    // visual helper (editor only)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
