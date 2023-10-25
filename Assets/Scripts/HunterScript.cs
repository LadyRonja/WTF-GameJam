using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class HunterScript : MonoBehaviour
{
    public float speed;
    public Transform target;
    public SpriteRenderer spriteRenderer; 
    Collider2D col;
    Rigidbody2D rb2D;
    
    Vector2 rayDirection;    

    public float checkPosTimer;       
    public bool movingRight = true;
    public bool movingRightLastFrame = true;
    public bool upsideDown;  
    
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = target.position - transform.position;
        if (upsideDown)
            speed = 7;
        else
            speed = 3;
        if (direction.x < 0)
        {
            movingRight = false;
            rayDirection = Vector2.left;
        }
        else
        {
            movingRight = true;
            rayDirection = Vector2.right;
        }
        if (movingRight != movingRightLastFrame)
        {
            spriteRenderer.flipX = !movingRight;            
        }
        movingRightLastFrame = movingRight;
        
        checkPosTimer += Time.deltaTime;        
        
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if (checkPosTimer >= 1)
        {        
            if(math.abs(rb2D.position.x - target.position.x) <= 0.6f)
                GravityFlip();
            
            RaycastHit2D wallInfo = Physics2D.Raycast(rb2D.position, rayDirection, (col.bounds.size.x) + 0.2f);
            Debug.DrawRay(rb2D.position, Vector2.right, Color.red, 0.05f);
            if (wallInfo == true)
            {
                if (wallInfo.collider.CompareTag("Player"))                
                    Debug.Log("Hit");                
                else                
                    GravityFlip(); 
            }            
            checkPosTimer = 0;
        }
    }
    private void GravityFlip()
    {
        rb2D.gravityScale *= -1;        
        upsideDown = !upsideDown;
        spriteRenderer.flipY = upsideDown;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {       
        if (other.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
        {           
                damagable.Die();            
        }        
    }
}
