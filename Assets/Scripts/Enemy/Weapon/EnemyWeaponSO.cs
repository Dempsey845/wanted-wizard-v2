using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Weapon")]
public class EnemyWeaponSO : ScriptableObject
{
    public float UseRate = 0.5f;
    public GameObject WeaponPrefab;
}
