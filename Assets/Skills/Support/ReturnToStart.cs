using UnityEngine;

public class ReturnToStart : MonoBehaviour
{
    public bool enableReturn = true;
    public float returnDuration = 0.5f;

    [HideInInspector] public Vector3 startPosition;
}
