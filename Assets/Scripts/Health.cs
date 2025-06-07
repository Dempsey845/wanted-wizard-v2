using System;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public int MaxHealth { get; private set; }

    public int CurrentHealth { get; private set; }

    public void Init(int startHealth, int maxHealth)
    {
        if (startHealth > maxHealth) startHealth = maxHealth;

        CurrentHealth = startHealth;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Took damage");
        CurrentHealth -= damage;
        OnTakeDamage();

        if (CurrentHealth <= 0) OnDeath();
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;

        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;

        OnHeal();
    }

    public virtual void OnTakeDamage()
    {
        throw new NotImplementedException();
    }

    public virtual void OnHeal()
    {
        throw new NotImplementedException();
    }

    public virtual void OnDeath()
    {
        throw new NotImplementedException();
    }
}
