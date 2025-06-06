using UnityEngine;

public class EnemyGun : EnemyWeapon
{
    [SerializeField] EnemyGunSO enemyGunSO;
    [SerializeField] Transform firePoint;
    [SerializeField] ParticleSystem muzzleFlashParticleSystem;

    public override void UseWeapon()
    {
        if (WeaponCoolingDown || !CanUseWeapon) return;

        base.UseWeapon();

        muzzleFlashParticleSystem.Play();
        Instantiate(enemyGunSO.BulletSO.BulletPrefab, firePoint.position, firePoint.rotation);
    }
}
