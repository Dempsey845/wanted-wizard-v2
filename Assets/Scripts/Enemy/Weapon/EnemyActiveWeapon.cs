using UnityEngine;

public class EnemyActiveWeapon : MonoBehaviour
{
    [SerializeField] EnemyWeaponSO startWeaponSO;

    public EnemyWeaponSO CurrentWeaponSO { get; private set; }
    public EnemyWeapon CurrentWeapon { get; private set; }

    void Start()
    {
        SwitchWeapon(startWeaponSO);
    }

    public void SwitchWeapon(EnemyWeaponSO newWeaponSO)
    {
        if (CurrentWeapon)
        {
            Destroy(CurrentWeapon.gameObject);
        }

        EnemyWeapon newWeapon = Instantiate(newWeaponSO.WeaponPrefab, transform.position, Quaternion.identity, this.transform).GetComponent<EnemyWeapon>();

        CurrentWeapon = newWeapon;
        CurrentWeaponSO = newWeaponSO;
    }

    public void UseWeapon()
    {
        CurrentWeapon.UseWeapon();
    }
}
