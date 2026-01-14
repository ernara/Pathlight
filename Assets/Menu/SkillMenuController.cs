using UnityEngine;
using UnityEngine.InputSystem;

public class SkillMenuController : MonoBehaviour
{
    public GameObject skillMenu;

    void Update()
    {
        if (Keyboard.current.kKey.wasPressedThisFrame || Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            skillMenu.SetActive(!skillMenu.activeSelf);
        }
    }
}