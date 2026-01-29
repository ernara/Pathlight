using UnityEngine;
using UnityEngine.InputSystem;

public class IceColdPulse : MonoBehaviour
{
    public float range = 10f;
    public LayerMask enemyLayer;

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
        Debug.Log("Pulse position: " + transform.position);

        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            range,
            enemyLayer
        );

        Debug.Log("Found colliders: " + hits.Length);

        foreach (var h in hits)
        {
            Debug.Log(
                "Hit object: " + h.name +
                " distance=" +
                Vector3.Distance(transform.position, h.transform.position)
            );
        }

        if (hits.Length == 0)
            return;

        Enemy2 enemy2 = hits[0].GetComponent<Enemy2>();
        if (enemy2 != null)
        {
            enemy2.Hit();
        }
        else
        {
            Debug.Log("No Enemy2 component found");
        }
    }

    // visual helper (editor only)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
