using UnityEngine;

public class RotateWeaponTowardsPlayer : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] float weaponRotationSpeed = 10f;

    [Header("Debug")]
    [SerializeField] bool showDebug = false;

    Transform weaponPivotPoint;
    Enemy enemy;

    public void Init(Enemy enemy, Transform weaponPivotPoint)
    {
        this.enemy = enemy;
        this.weaponPivotPoint = weaponPivotPoint;
    }

    void Update()
    {
        if (!enemy || !enemy.Player || !weaponPivotPoint) return;

        Transform playerTransform = enemy.Player.transform;
        
        if (enemy.PlayerInFOV)
        {
            RotateWeaponTowards(playerTransform.position);
            if (showDebug) Debug.Log("Player in view");
        }
        else
        {
            weaponPivotPoint.rotation = Quaternion.Slerp(weaponPivotPoint.rotation, Quaternion.identity, Time.deltaTime * weaponRotationSpeed);
        }
    }

    void RotateWeaponTowards(Vector3 targetPosition)
    {
        Vector3 directionToPlayer = new(targetPosition.x - weaponPivotPoint.position.x, 0, targetPosition.z - weaponPivotPoint.position.z);

        if (directionToPlayer.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);

            weaponPivotPoint.rotation = Quaternion.Slerp(weaponPivotPoint.rotation, targetRotation, Time.deltaTime * weaponRotationSpeed);
        }
    }
}
