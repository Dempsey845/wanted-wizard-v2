using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Bullet")]
public class EnemyBulletSO : ScriptableObject
{
    public GameObject bulletPrefab;
    public float moveSpeed = 20f;
}
