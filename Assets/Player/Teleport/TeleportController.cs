using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TeleportController : MonoBehaviour
{
    [HideInInspector] public Teleport currentTeleport;

    void Update()
    {
        if (currentTeleport != null && Keyboard.current.dKey.wasPressedThisFrame)
        {
            Debug.Log("D pressed, loading scene: " + currentTeleport.targetSceneName);
            SceneManager.LoadScene(currentTeleport.targetSceneName);
        }
    }
}
