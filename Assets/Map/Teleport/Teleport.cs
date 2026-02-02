using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Header("Scene to load when player presses D inside trigger")]
    public string targetSceneName;

    [HideInInspector] public bool playerInside = false;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Teleport trigger entered by: " + other.name + " | targetSceneName=" + targetSceneName);
        playerInside = true;

        // Set current teleport in player
        TeleportController tc = other.GetComponentInParent<TeleportController>();
        if (tc != null) tc.currentTeleport = this;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Safety: only clear if player actually moved away
        if (Vector3.Distance(transform.position, other.transform.position) > 1.5f)
        {
            Debug.Log("Player left teleport: " + gameObject.name);
            playerInside = false;

            TeleportController tc = other.GetComponentInParent<TeleportController>();
            if (tc != null && tc.currentTeleport == this)
                tc.currentTeleport = null;
        }
    }

    // Optional: visualize teleport radius in editor
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
