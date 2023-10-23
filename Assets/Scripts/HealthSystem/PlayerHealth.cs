using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    public override void Die()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerArtifactHandler>().enabled = false;
        GetComponent<SpriteRenderer>().color = Color.red;

        // TODO:
        // implement Respawn or Game over screen
    }
}
