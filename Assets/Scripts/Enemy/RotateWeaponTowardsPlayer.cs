using UnityEngine;

public class RotateWeaponTowardsPlayer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform weaponPivotPoint;

    [Header("Rotation Settings")]
    [SerializeField] float weaponRotationSpeed = 10f;
    [SerializeField, Range(0f, 180f)] float maxViewAngle = 180f;

    [Header("Debug")]
    [SerializeField] bool showDebug = false;

    Transform playerTransform;

    void Start()
    {
        Player player = FindFirstObjectByType<Player>();
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found in scene.");
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        Vector3 directionToPlayer = playerTransform.position - transform.parent.position;
        float angleToPlayer = Vector3.Angle(transform.parent.forward, directionToPlayer);

        if (angleToPlayer <= maxViewAngle)
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
