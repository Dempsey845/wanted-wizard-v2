using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MonsterProjector : MonoBehaviour
{
    [SerializeField] GameObject monsterPrefab;
    [SerializeField] Transform firePoint;
    [SerializeField] float attackRate = 1f;

    bool canAttack = true;

    InputAction attackInput;

    void OnEnable()
    {
        attackInput = InputSystem.actions.FindAction("Attack");
        attackInput.Enable();
        attackInput.performed += OnAttackPerformed;
    }

    void OnDisable()
    {
        if (attackInput != null)
        {
            attackInput.performed -= OnAttackPerformed;
            attackInput.Disable();
        }
    }

    void OnDestroy()
    {
        if (attackInput != null)
        {
            attackInput.performed -= OnAttackPerformed;
        }
    }

    void OnAttackPerformed(InputAction.CallbackContext context)
    {
        Attack();
    }

    void Attack()
    {
        if (!canAttack) return;

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 direction = ray.direction;

        Instantiate(monsterPrefab, firePoint.position, Quaternion.LookRotation(direction));
        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackRate);
        canAttack = true;
    }
}
