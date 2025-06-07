using System.Collections;
using UnityEngine;

public class MonsterProjectile : Projectile
{
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float _maxLifetime = 5f;
    [SerializeField] float firebackDelay = 2f;
    [SerializeField] float absorbRadius = 5f;
    [SerializeField] LayerMask enemyProjectileLayer;
    [SerializeField] float spreadAngle = 5f;
    [SerializeField] int reverseFireWeight = 4;
    [SerializeField] int randomFireWeight = 1;

    bool canFireback = true;

    public override void Start()
    {
        base.Init(projectileSpeed, _maxLifetime);
        base.Start();
    }

    public override void Update()
    {
        Collider[] collidersInAbsorbRadius = Physics.OverlapSphere(transform.position, absorbRadius, enemyProjectileLayer, QueryTriggerInteraction.Collide);
        if (collidersInAbsorbRadius.Length > 0 && canFireback)
        {
            foreach (Collider collider in collidersInAbsorbRadius)
            {
                if (collider.TryGetComponent(out EnemyBullet enemyBullet))
                {
                    HandleEnemyProjectileCollision(collider, enemyBullet);
                }
            }
        }

        base.Update();
    }

    void HandleEnemyProjectileCollision(Collider collider, EnemyBullet enemyBullet)
    {
        BulletManager.Instance.AddBullet(enemyBullet.bulletSO);

        Fireback(collider, enemyBullet);
        Destroy(collider.gameObject);
    }

    void Fireback(Collider collider, EnemyBullet enemyBullet)
    {
        if (!canFireback) return;

        // If true fireback the projectiles back towards the enemy, else fire in random directions
        bool firebackInReverseDirection = Random.Range(0, reverseFireWeight + randomFireWeight) < reverseFireWeight;

        Vector3 origin = collider.transform.position;

        if (firebackInReverseDirection)
        {
            // Fire in reverse direction of the incoming bullet
            FireInReverseDirection(enemyBullet, origin);
        }
        else
        {
            FireInRandomDirection(enemyBullet, origin);
        }

        StartCoroutine(FirebackCooldown());
    }

    void FireInRandomDirection(EnemyBullet enemyBullet, Vector3 origin)
    {
        Vector3[] directions = GetRandomDirections(
                        BulletManager.Instance.GetMaxBulletCount(),
                        BulletManager.Instance.GetFirebackRadius(),
                        origin
                    );

        foreach (Vector3 direction in directions)
        {
            Debug.DrawRay(origin, direction * 5f, Color.red, 5f);
            Instantiate(enemyBullet.bulletSO.BulletPrefab, origin, Quaternion.LookRotation(direction));
        }
    }

    void FireInReverseDirection(EnemyBullet enemyBullet, Vector3 origin)
    {
        Vector3 reverseDirection = -enemyBullet.GetComponent<Rigidbody>().linearVelocity.normalized;

        if (reverseDirection != Vector3.zero)
        {
            int count = BulletManager.Instance.GetMaxBulletCount() / 2;

            for (int i = 0; i < count; i++)
            {
                // Calculate slight spread using rotation offset
                float angleOffset = (i - (count - 1) / 2f) * spreadAngle;
                Quaternion rotationOffset = Quaternion.AngleAxis(angleOffset, Vector3.up);
                Vector3 spreadDirection = rotationOffset * reverseDirection;

                Debug.DrawRay(origin, spreadDirection * 5f, Color.green, 5f);
                Instantiate(enemyBullet.bulletSO.BulletPrefab, origin, Quaternion.LookRotation(spreadDirection));
            }
        }
    }

    IEnumerator FirebackCooldown()
    {
        canFireback = false;
        yield return new WaitForSeconds(firebackDelay);
        canFireback = true;
    }

    Vector3[] GetRandomDirections(int count, float radius, Vector3 origin)
    {
        Vector3[] points = new Vector3[count];

        for (int i = 0; i < count; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * radius;

            Vector3 spawnPoint = origin + randomOffset;

            Vector3 direction = (spawnPoint - origin).normalized;

            points[i] = direction;
        }

        return points;
    }

}
