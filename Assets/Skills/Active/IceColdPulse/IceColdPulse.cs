using UnityEngine;
using UnityEngine.InputSystem;

public class IceColdPulse : MonoBehaviour
{
    public GameObject pulsePrefab;
    public float lifetime = 5f;

    void Awake()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        foreach (var mr in renderers)
        {
            mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            mr.receiveShadows = false;
        }
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.cKey.wasPressedThisFrame)
        {
            CastPulse();
        }
    }

    void CastPulse()
    {
        if (pulsePrefab == null) return;

        Vector3 spawnPos = transform.position;

        GameObject pulse = Instantiate(pulsePrefab, spawnPos, Quaternion.identity);

        Destroy(pulse, lifetime);
    }
}