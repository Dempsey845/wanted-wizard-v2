using UnityEngine;

public abstract class EnemyWeapon : MonoBehaviour
{
    [SerializeField] EnemyWeaponSO weaponSO;

    public virtual void UseWeapon()
    {
        Debug.Log("Weapon Used");
    }
}
