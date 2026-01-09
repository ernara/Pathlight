using UnityEngine;
using UnityEngine.InputSystem;


public class WASDMoveNew : MonoBehaviour
{
    public GameObject fireballPrefab;
    PlayerInput input; // The generated C# class
    Vector2 move;

    public float dashDistance = 3f;

    void Awake()
    {
        input = new PlayerInput(); // Initialize the input
    }

    void OnEnable()
    {
        input.Player.Enable(); // Enable the action map

        input.Player.@Move.performed += ctx => {
            move = ctx.ReadValue<Vector2>();
            Debug.Log("Move input: " + move); // debug log
        };

        input.Player.@Move.canceled += ctx => move = Vector2.zero;
    }

    void OnDisable()
    {
        input.Player.Disable();
    }

    void Update()
    {
        if (Keyboard.current.wKey.isPressed) Debug.Log("W pressed");
        if (Keyboard.current.aKey.isPressed) Debug.Log("A pressed");
        if (Keyboard.current.sKey.isPressed) Debug.Log("S pressed");
        if (Keyboard.current.dKey.isPressed) Debug.Log("D pressed");
        if (move != Vector2.zero)
            Debug.Log("Move vector: " + move);

        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            transform.position += transform.forward * dashDistance;
            Debug.Log("Dash!");
        }

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            Instantiate(
                fireballPrefab,
                transform.position + transform.forward,
                transform.rotation
            );
        }

        transform.Translate(new Vector3(move.x, 0, move.y) * 5f * Time.deltaTime);
    }
}
