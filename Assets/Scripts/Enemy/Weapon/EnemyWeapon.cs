using System.Collections;
using UnityEngine;

public abstract class EnemyWeapon : MonoBehaviour
{
    public Transform WeaponPivot;
    public EnemyWeaponSO WeaponSO;
    public bool WeaponCoolingDown { get { return weaponCoolingDown; } }
    public bool CanUseWeapon { get { return canUseWeapon; } set { canUseWeapon = value; } }

    bool weaponCoolingDown = false;
    bool canUseWeapon = true;

    public virtual void UseWeapon()
    {
        StartCoroutine(WeaponCooldown());
    }

    IEnumerator WeaponCooldown()
    {
        weaponCoolingDown = true;
        yield return new WaitForSeconds(WeaponSO.UseRate);
        weaponCoolingDown = false;
    }
}
