using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : Health
{
    bool dead = false;

    public override void Die()
    {
        if (dead)
            return;

        dead = true;
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerArtifactHandler>().enabled = false;
        GetComponent<SpriteRenderer>().color = Color.red;
        Destroy(GetComponent<Rigidbody2D>());

        if (GameOverManager.Instance != null)
            GameOverManager.Instance.GameIsOver();
    }
}
