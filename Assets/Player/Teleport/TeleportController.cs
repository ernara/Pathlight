using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class TeleportController : MonoBehaviour
{
    [HideInInspector] public Teleport currentTeleport;

    bool shouldBoostAfterLoad = false;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if (currentTeleport != null && Keyboard.current.dKey.wasPressedThisFrame)
        {
            shouldBoostAfterLoad = true;
            SceneManager.LoadScene(currentTeleport.targetSceneName);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!shouldBoostAfterLoad) return;

        shouldBoostAfterLoad = false;

        TeleportSpeedBoost boost = GetComponent<TeleportSpeedBoost>();

        if (boost != null)
            boost.ActivateBoost(5f);
    }
}
