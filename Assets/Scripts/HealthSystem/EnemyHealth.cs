using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{

    public override void Die()
    {
        StartCoroutine(Death());
    }

    private IEnumerator Death()
    {
        // Disable Enemy behaivor
        // Play death sound
        // Play death animation
        // yield return new WaitForSeconds(/*death animation time*/);
        base.Die();
        return null;
    }
}
