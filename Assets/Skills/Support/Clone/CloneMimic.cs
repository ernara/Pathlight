using UnityEngine;

public class CloneMimic : MonoBehaviour
{
    GameObject original;
    ClickMove cloneMove;
    ClickMove originalMove;

    Animator cloneAnimator;
    Animator originalAnimator;

    // Optional: add skill mimic here if needed
    Dash originalDash;
    Dash cloneDash;

    void Awake()
    {
        // Get clone components
        cloneMove = GetComponent<ClickMove>();
        cloneAnimator = GetComponentInChildren<Animator>();
        cloneDash = GetComponent<Dash>();
    }

    // Call this right after spawning clone
    public void Initialize(GameObject player)
    {
        original = player;

        // Get original player components
        originalMove = original.GetComponent<ClickMove>();
        originalAnimator = original.GetComponentInChildren<Animator>();
        originalDash = original.GetComponent<Dash>();

        // Make clone visually distinct
        var renderers = GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
        {
            r.material = new Material(r.material); // create new material instance
            r.material.color = Color.cyan;
        }
    }

    void Update()
    {
        if (original == null) return;

        // Mimic movement
        if (originalMove != null && cloneMove != null)
        {
            cloneMove.enabled = false; // disable clone input
            transform.position = original.transform.position;
            transform.rotation = original.transform.rotation;
        }

        // Mimic animations
        if (originalAnimator != null && cloneAnimator != null)
        {
            cloneAnimator.SetBool("IsMoving", originalAnimator.GetBool("IsMoving"));
        }

        // Mimic Dash usage
        if (originalDash != null && cloneDash != null)
        {
            // If player just dashed, make clone dash too
            if (originalDash.DashTriggered) // you need to add this bool in Dash
            {
                cloneDash.PerformDash();
            }
        }

        // You can add shooting mimic here if your skills have an IsFiring() or similar
    }
}
