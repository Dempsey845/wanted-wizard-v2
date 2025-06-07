using UnityEngine;

public class MonsterProjectile : Projectile
{
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float maxLifetime = 5f;

    public override void Start()
    {
        base.Init(projectileSpeed, maxLifetime);
        base.Start();
    }

    void OnTriggerEnter(Collider other)
    {
        //Destroy(this.gameObject);
    }
}
