using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MonsterProjector : MonoBehaviour
{
    [SerializeField] GameObject monsterPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float attackRate = 1f;

    [SerializeField] float maxOffsetForFireback = 0.1f;

    bool canAttack = true;

    InputAction attackInput;
    InputAction firebackInput;

    void OnEnable()
    {
        attackInput = InputSystem.actions.FindAction("Attack");
        attackInput.Enable();
        attackInput.performed += OnAttackPerformed;

        firebackInput = InputSystem.actions.FindAction("Fireback");
        firebackInput.Enable();
        firebackInput.performed += OnFirebackPerformed;
    }


    void OnDisable()
    {
        if (attackInput != null)
        {
            attackInput.performed -= OnAttackPerformed;
            attackInput.Disable();
        }

        if (firebackInput != null)
        {
            firebackInput.performed -= OnFirebackPerformed;
            firebackInput.Disable();
        }
    }

    void OnDestroy()
    {
        if (attackInput != null)
        {
            attackInput.performed -= OnAttackPerformed;
        }

        if (firebackInput != null)
        {
            firebackInput.performed -= OnFirebackPerformed;
        }
    }

    void OnFirebackPerformed(InputAction.CallbackContext context)
    {
        Fireback();
    }

    void OnAttackPerformed(InputAction.CallbackContext context)
    {
        Attack();
    }

    void Attack()
    {
        if (!canAttack) return;

        Vector3 direction = GetDiectionToCameraCenter();

        Instantiate(monsterPrefab, firePoint.position, Quaternion.LookRotation(direction));
        StartCoroutine(AttackCooldown());
    }

    static Vector3 GetDiectionToCameraCenter()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 direction = ray.direction;
        return direction;
    }

    void Fireback()
    {
        Debug.Log("Fireback pressed");

        List<EnemyBulletSO> enemyBulletsSOs = BulletManager.Instance.enemyBulletSOs;

        if (enemyBulletsSOs.Count < BulletManager.Instance.GetMaxBulletCount()) return;

        Debug.Log("Fireback initiated...");


        foreach (EnemyBulletSO bulletSO in enemyBulletsSOs)
        {
            Vector3 direction = GetRandomDirection();

            Instantiate(bulletSO.BulletPrefab, firePoint.position, Quaternion.LookRotation(direction));

            BulletManager.Instance.enemyBulletSOs = new();
            BulletManager.Instance.UpdateCountText();
        }
    }

    Vector3 GetRandomDirection()
    {
        Vector3 direction = GetDiectionToCameraCenter();
        float xDirectionOffset = Random.Range(-maxOffsetForFireback, maxOffsetForFireback);
        float yDirectionOffset = Random.Range(-maxOffsetForFireback, maxOffsetForFireback);
        direction.x += xDirectionOffset;
        direction.y += yDirectionOffset;
        return direction;
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }
}
