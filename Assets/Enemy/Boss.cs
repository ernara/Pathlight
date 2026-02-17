public class Boss : EnemyBase
{
    protected override void Awake()
    {
        maxHits = 20; 
        base.Awake();
    }

    protected override void Die()
    {
        base.Die();
    }
}
