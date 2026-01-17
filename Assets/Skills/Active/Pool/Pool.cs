using UnityEngine;
using UnityEngine.InputSystem;

public class Pool : MonoBehaviour
{
    public GameObject poolPrefab;
    public float duration = 3f;

    void Update()
    {
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            CastPool();
        }
    }

    //void CastPool()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
    //    int groundMask = LayerMask.GetMask("Ground");

    //    if (!Physics.Raycast(ray, out RaycastHit hit, 1000f, groundMask))
    //        return;

    //    Vector3 pos = new Vector3(hit.point.x, 1.01f, hit.point.z);

    //    GameObject pool = Instantiate(
    //        poolPrefab,
    //        pos,
    //        Quaternion.identity
    //    );

    //    Destroy(pool, duration);
    //}

    void CastPool()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!Physics.Raycast(ray, out RaycastHit hit, 1000f))
            return;


        Debug.Log("Pool hit point: " + hit.point);

        float groundTop = hit.collider.bounds.max.y;
        Vector3 pos = new Vector3(hit.point.x, groundTop + 0.02f, hit.point.z);

        GameObject pool = Instantiate(poolPrefab, pos, Quaternion.identity);
        Destroy(pool, duration);
    }

}
