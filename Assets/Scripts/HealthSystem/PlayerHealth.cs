using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : Health
{
    public override void Die()
    {
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<PlayerArtifactHandler>().enabled = false;
        GetComponent<SpriteRenderer>().color = Color.red;
        Destroy(GetComponent<Rigidbody2D>());
        

        // TODO:
        // implement Respawn or Game over screen
    }
}
