using UnityEngine;
using System.Collections;

public class TeleportSpeedBoost : MonoBehaviour
{
    ClickMove move;

    float flat = 10f;
    float percent = 0f;

    bool boostActive = false;

    void Awake()
    {
        move = GetComponent<ClickMove>();
    }

    public void ActivateBoost(float duration = 5f)
    {
        Debug.Log("BOOST ACTIVATED");

        if (boostActive)
        {
            move.RemoveSpeedModifier(flat, percent);
            StopAllCoroutines();
        }

        StartCoroutine(BoostRoutine(duration));
    }

    IEnumerator BoostRoutine(float duration)
    {
        boostActive = true;

        move.AddSpeedModifier(flat, percent);

        yield return new WaitForSeconds(duration);

        move.RemoveSpeedModifier(flat, percent);

        boostActive = false;
    }
}
