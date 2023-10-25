using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDisplay : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float offSet = 0.5f;
    Vector2 direction = Vector2.zero;

    private void Update()
    {
        if (GameSettings.usingGamepad)
        {
            float x = Input.GetAxis("HorAimController");
            float y = Input.GetAxis("VerAimController");

            if (x != 0 && y != 0)
                direction = new Vector2(x, y);
        }
        else
        {
            direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position);
        }
        direction.Normalize();
        transform.up = direction;
        transform.localPosition = transform.up * offSet;
    }
}
