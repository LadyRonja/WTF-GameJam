using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArtifactHandler : MonoBehaviour
{
    [SerializeField] Artifact artifact;
    bool isCarrying = false;

    [Header("Throwing")]
    public float throwSpeed = 15f;

    [Header("Pickup")]
    [SerializeField] float pickupRadius = 0.8f;
    [SerializeField] float pickUpCooldown = 1f;
    float pickUpTimer;

    private void Update()
    {
        if (artifact == null)
            return;

        PickupManager();
        CarryManager();
        ThrowManager();
    }

    private void CarryManager()
    {
        if (!isCarrying) return;

       artifact.rb.isKinematic = false;

       artifact.transform.position = this.transform.position;
    }
    
    private void ThrowManager()
    {
        if(!isCarrying) return;

        if (Input.GetButtonDown("Fire"))
        {
            Vector2 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
            dir.Normalize();

            Throw(dir);
        }
        else if (Input.GetButtonDown("FireController"))
        {
            Vector2 dir = Vector2.zero;
            dir.x = Input.GetAxis("HorAimController");
            dir.y = Input.GetAxis("VerAimController");

            if(dir == Vector2.zero)
            {
                dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
                dir.Normalize();
            }
            Throw(dir);
        }

        void Throw(Vector2 direction)
        {
            pickUpTimer = pickUpCooldown;
            isCarrying = false;
            artifact.rb.gravityScale = 1;
            artifact.Throw(direction, throwSpeed);
        }
    }

    private void PickupManager()
    {
        if (!isCarrying)
        { 
            pickUpTimer -= Time.deltaTime;
            if (pickUpTimer > 0) return;

            Vector2 dist = artifact.transform.position - transform.position;
            if (Mathf.Abs(dist.sqrMagnitude) < pickupRadius * pickupRadius)
            {
                isCarrying = true;
                artifact.rb.gravityScale = 0;
            }
        }
    }

}
