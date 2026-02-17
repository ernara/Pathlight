using UnityEngine;

public class Monster : EnemyBase
{
    [SerializeField] private int monsterMaxHits = 4;

    protected override void Awake()
    {
        maxHits = monsterMaxHits;
        base.Awake();
    }
}
