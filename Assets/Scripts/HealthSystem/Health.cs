using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Health : MonoBehaviour, IDamagable
{
    public int maxHealth = 1;
    public int currentHealth = 1;
    public bool invulnerable = false;

    public virtual void TakeDamage(int amount)
    {
        if(!invulnerable)
        {
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
        }
    }
    public virtual void Die()
    {
        currentHealth = 0;
        Destroy(this.gameObject);
    }


}
