using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class Artifact : MonoBehaviour
{
    public int damage = 1;
    [HideInInspector] public bool damageOnHit = false;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Collider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if(rb.velocity != Vector2.zero)
            transform.up = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(damageOnHit) 
        { 
            if(other.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
            {
                damageOnHit = false;
                damagable.TakeDamage(damage);
            }
        }
    }

    public void Throw(Vector2 direction, float force)
    {
        transform.up = (Vector3)direction;
        rb.velocity = direction * force;
        damageOnHit= true;
    }
}
