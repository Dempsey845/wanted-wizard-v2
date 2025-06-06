using UnityEngine;

public class TestEnemy : Enemy
{
    public override void Update()
    {
        base.Update();

        Attack();
    }

    public override void Attack()
    {
        if (!CanAttack) return;
        base.Attack();

        EnemyActiveWeapon.UseWeapon();
    }   
}
