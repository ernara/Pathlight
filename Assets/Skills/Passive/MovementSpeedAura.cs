using UnityEngine;

public class MovementSpeedAura : MonoBehaviour
{
    public float flatSpeedBonus = 100f;
    [Range(0f, 5f)]
    public float percentBonus = 0.30f; // 30%

    ClickMove move;

    void OnEnable()
    {
        move = GetComponent<ClickMove>();

        if (move != null)
            move.AddSpeedModifier(flatSpeedBonus, percentBonus);
    }

    void OnDisable()
    {
        if (move != null)
            move.RemoveSpeedModifier(flatSpeedBonus, percentBonus);
    }
}
