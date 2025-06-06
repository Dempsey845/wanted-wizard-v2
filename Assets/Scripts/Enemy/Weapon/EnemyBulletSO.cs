using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Bullet")]
public class EnemyBulletSO : ScriptableObject
{
    public GameObject BulletPrefab;
    public GameObject BulletHitFXPrefab;
    public float moveSpeed = 20f;
    public float maxLifetime = 5f;
}
