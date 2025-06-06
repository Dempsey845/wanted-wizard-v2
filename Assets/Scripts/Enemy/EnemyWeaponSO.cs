using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Weapon")]
public class EnemyWeaponSO : ScriptableObject
{
    public float FireRate = 0.5f;
    public EnemyBulletSO BulletSO; 
}
