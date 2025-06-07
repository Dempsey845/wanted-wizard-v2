using UnityEngine;

public class EnemyBullet : Projectile
{
    public EnemyBulletSO bulletSO;

    const string PLAYER_TAG = "Player";
    const string ENEMY_BULLET_TAG = "Enemy Bullet";

    public override void Start()
    {
        Init(bulletSO.moveSpeed, bulletSO.maxLifetime);
        base.Start();
    } 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ENEMY_BULLET_TAG)) return;

        if (other.CompareTag(PLAYER_TAG))
        {
            // TODO
        }
        
        Instantiate(bulletSO.BulletHitFXPrefab, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
