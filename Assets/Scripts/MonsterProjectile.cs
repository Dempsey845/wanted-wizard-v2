using UnityEngine;

public class MonsterProjectile : Projectile
{
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float _maxLifetime = 5f;
    [SerializeField] float absorbRadius = 5f;
    [SerializeField] LayerMask enemyProjectileLayer;

    public override void Start()
    {
        base.Init(projectileSpeed, _maxLifetime);
        base.Start();
    }

    void Update()
    {
        Collider[] collidersInAbsorbRadius = Physics.OverlapSphere(transform.position, absorbRadius, enemyProjectileLayer, QueryTriggerInteraction.Collide);
        if (collidersInAbsorbRadius.Length > 0)
        {
            foreach (Collider collider in collidersInAbsorbRadius)
            {
                if (collider.TryGetComponent(out EnemyBullet enemyBullet))
                {
                    BulletManager.Instance.AddBullet(enemyBullet.bulletSO);
                    Destroy(collider.gameObject);
                }
            }
        }
    }
}
