using UnityEngine;

public class Teleport : MonoBehaviour
{
    public string targetSceneName;

    [HideInInspector] public bool playerInside = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Teleport trigger entered by: " + other.name + " | targetSceneName=" + targetSceneName);
        playerInside = true;

        TeleportController tc = other.GetComponentInParent<TeleportController>();
        if (tc != null) tc.currentTeleport = this;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (Vector3.Distance(transform.position, other.transform.position) > 1.5f)
        {
            Debug.Log("Player left teleport: " + gameObject.name);
            playerInside = false;

            TeleportController tc = other.GetComponentInParent<TeleportController>();
            if (tc != null && tc.currentTeleport == this)
                tc.currentTeleport = null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            if (col is SphereCollider sphere)
                Gizmos.DrawWireSphere(sphere.center, sphere.radius);
            else if (col is BoxCollider box)
                Gizmos.DrawWireCube(box.center, box.size);
        }
    }
}
