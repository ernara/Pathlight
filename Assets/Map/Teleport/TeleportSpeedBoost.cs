using UnityEngine;
using System.Collections;

public class TeleportSpeedBoost : MonoBehaviour
{
    ClickMove move;

    float flat = 10f;
    float percent = 0f; 

    void Awake()
    {
        move = GetComponent<ClickMove>();
    }

    public void ActivateBoost(float duration = 5f)
    {
        Debug.Log("BOOST ACTIVATED");
        StopAllCoroutines();
        StartCoroutine(BoostRoutine(duration));
    }

    IEnumerator BoostRoutine(float duration)
    {
        move.AddSpeedModifier(flat, percent);

        yield return new WaitForSeconds(duration);

        move.RemoveSpeedModifier(flat, percent);
    }
}
