using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float h = 0f;
        float v = 0f;

        if (Input.GetKey(KeyCode.W)) v += 1;
        if (Input.GetKey(KeyCode.S)) v -= 1;
        if (Input.GetKey(KeyCode.D)) h += 1;
        if (Input.GetKey(KeyCode.A)) h -= 1;

        Vector3 move = new Vector3(h, 0, v).normalized;
        transform.position += move * moveSpeed * Time.deltaTime;
    }
}
