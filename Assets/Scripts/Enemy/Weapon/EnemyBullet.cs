using UnityEngine;

public class EnemyBullet : Projectile
{
    public EnemyBulletSO bulletSO;

    const string PLAYER_TAG = "Player";
    const string ENEMY_BULLET_TAG = "Enemy Bullet";
    const string ENEMY_TAG = "Enemy";

    public override void Start()
    {
        Init(bulletSO.moveSpeed, bulletSO.maxLifetime);
        base.Start();
    } 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ENEMY_BULLET_TAG)) return;

        if (other.CompareTag(ENEMY_TAG))
        {
            if (other.gameObject.TryGetComponent(out EnemyHealth enemyHealth))
            {
                enemyHealth.TakeDamage(bulletSO.Damage);
            }
        }

        if (other.CompareTag(PLAYER_TAG))
        {
            // TODO
        }
        
        Instantiate(bulletSO.BulletHitFXPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
