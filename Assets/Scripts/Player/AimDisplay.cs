using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDisplay : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float offSet = 0.5f;

    private void Update()
    {
        Vector2 dir = Vector2.zero;

        dir.x = Input.GetAxis("HorAimController");
        dir.y = Input.GetAxis("VerAimController");
        if (dir == Vector2.zero)
        {
            dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        }
        dir.Normalize();
        transform.up = dir;
        transform.localPosition = transform.up * offSet;
    }
}
