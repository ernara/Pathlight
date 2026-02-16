using UnityEngine;

public class Spell : MonoBehaviour
{
    public int damage = 1;
    public bool damageOverTime = false;
    public float tickInterval = 1f;

    [HideInInspector]
    public float radius; 

    void Awake()
    {
        Collider col = GetComponent<Collider>();

        float baseRadius;

        if (col != null)
        {
            baseRadius = Mathf.Max(col.bounds.extents.x, col.bounds.extents.z);
        }
        else
        {
            baseRadius = Mathf.Max(transform.localScale.x, transform.localScale.z) * 0.5f;
        }

        radius = baseRadius * 0.4f;

        
        radius = Mathf.Max(radius, 2f); 
    }
}
