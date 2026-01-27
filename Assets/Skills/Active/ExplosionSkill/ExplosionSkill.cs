using UnityEngine;
using UnityEngine.InputSystem;

public class ExplosionSkill : MonoBehaviour
{
    public Transform firePoint;
    public GameObject explosionPrefab;

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.xKey.wasPressedThisFrame)
        {
            SpawnExplosion();
        }
    }

    void SpawnExplosion()
    {
        Debug.Log("Explosion spawned");

        Vector3 pos = firePoint.position + firePoint.right * 1f;
        pos.z = firePoint.position.z + 0.1f; // in front of player

        GameObject explosion = Instantiate(explosionPrefab, pos, Quaternion.identity);

        // Ensure SpriteRenderer is visible
        SpriteRenderer sr = explosion.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.sortingLayerName = "Skills";
            sr.sortingOrder = 10;
        }
    }

}
