using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] int startHealth = 10;
    [SerializeField] int maxHealth = 10;

    void Awake()
    {
        Init(startHealth, maxHealth);
    }

    public override void OnDeath()
    {
        Destroy(this.gameObject);
    }

    public override void OnTakeDamage()
    {
        // TODO: IMPLEMENT :)
    }
}
