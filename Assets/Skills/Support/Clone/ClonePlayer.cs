using UnityEngine;

public class ClonePlayer : MonoBehaviour
{
    public GameObject clonePrefab; 
    public int cloneCount = 10;    
    public float spawnRadius = 5f;
    public float cloneDuration = 15f;

    void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.rKey.wasPressedThisFrame)
        {
            Debug.Log("Manual ActivateClone trigger");
            ActivateClone();
        }
    }

    public void ActivateClone()
    {
        if (clonePrefab == null)
        {
            Debug.LogError("clonePrefab is null!");
            return;
        }

        for (int i = 0; i < cloneCount; i++)
        {
            Vector2 offset = Random.insideUnitCircle * spawnRadius;
            Vector3 spawnPos = transform.position + new Vector3(offset.x, 0, offset.y);

            GameObject clone = Instantiate(clonePrefab, spawnPos, transform.rotation);
            clone.SetActive(true);

            var c = clone.GetComponent<ClonePlayer>();
            if (c != null) Destroy(c);

            var mimic = clone.AddComponent<CloneMimic>();
            mimic.Initialize(this.gameObject);

            clone.layer = gameObject.layer;
            foreach (Transform t in clone.GetComponentsInChildren<Transform>())
                t.gameObject.layer = gameObject.layer;

            Destroy(clone, cloneDuration);
        }

        Debug.Log(cloneCount + " clones spawned around player!");
    }
}
