using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public string targetSceneName;

    bool playerInside;

    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.up * 2, Color.red);
        Debug.Log("Player pos: " + transform.position + " | Teleport pos: " + transform.position + "playerInside = " + playerInside);

        if (playerInside && Keyboard.current.dKey.wasPressedThisFrame)
        {
            Debug.Log("playerInside = " + playerInside);
            Debug.Log("D pressed inside teleport. Loading scene: " + targetSceneName);
            SceneManager.LoadScene(targetSceneName);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Player entered teleport");
        playerInside = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Player left teleport");
        playerInside = false;
    }

}
