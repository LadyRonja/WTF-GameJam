using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum HunterState
{    
    Run,
    Jump,
    Digging,
}
public class HunterScript : MonoBehaviour
{
    public float speed;
    public Transform target;
    public GameObject hunterAnimation;
    public SkeletonAnimation skeletonAnimation;
    Collider2D col;
    Rigidbody2D rb2D;
    
    Vector2 rayDirection;
    Vector2 teleportPosition;

    float teleportTimer;

    public float checkPosTimer;       
    public bool movingRight = true;
    public bool movingRightLastFrame = true;
    public bool upsideDown;
    public bool grounded;

    HunterState state = HunterState.Run;
    HunterState lastState = HunterState.Run;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>(); 
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        teleportTimer += Time.deltaTime;
        if (math.abs(FastMath.Distance(transform.position, target.position)) > 20 && teleportTimer > 8)
        {
            teleportPosition = new Vector2(Camera.main.transform.position.x - 15, target.position.y);
            transform.position = teleportPosition;
        }
        GroundCheck();
        MovementManager();
    }

    private void MovementManager()
    {
        if (grounded)
            state = HunterState.Run;
        Vector2 direction = target.position - transform.position;
        if (upsideDown)
        {
            speed = 7;
            skeletonAnimation.timeScale = 2;
        }
        else
        {
            speed = 3;
            skeletonAnimation.timeScale = 1;
        }
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
            Vector3 turnAround = new Vector3(hunterAnimation.transform.localScale.x * -1, 
                hunterAnimation.transform.localScale.y, hunterAnimation.transform.localScale.z);
            hunterAnimation.transform.localScale = turnAround;
        }
        movingRightLastFrame = movingRight;

        checkPosTimer += Time.deltaTime;

        Vector2 move = rb2D.velocity;
        move.x = direction.normalized.x * speed;
        rb2D.velocity = move;
        if (checkPosTimer >= 0.5)
        {
            if (math.abs(rb2D.position.x - target.position.x) <= 1f)
                GravityFlip(direction);

            RaycastHit2D wallInfo = Physics2D.Raycast(rb2D.position, rayDirection, (col.bounds.size.x + 0.2f));
            Debug.DrawRay(rb2D.position, Vector2.right, Color.red, 0.2f);
            if (wallInfo == true)
            {
                if (!wallInfo.collider.CompareTag("Player"))
                    GravityFlip(direction);
                else
                    rb2D.AddForce(direction * 15f, ForceMode2D.Impulse);
            }
            checkPosTimer = 0;
        }
    }

    private void GravityFlip(Vector2 dir)
    {
        state = HunterState.Jump;
        rb2D.gravityScale *= -1;        
        upsideDown = !upsideDown;
        Vector3 turnUpsideDown = new Vector3(hunterAnimation.transform.localScale.x,
                hunterAnimation.transform.localScale.y * -1, hunterAnimation.transform.localScale.z);
        hunterAnimation.transform.localScale = turnUpsideDown;
        Vector3 turnUpsideDownPosition = new Vector3(hunterAnimation.transform.localPosition.x,
                hunterAnimation.transform.localPosition.y * -1, hunterAnimation.transform.localPosition.z);
        hunterAnimation.transform.localPosition = turnUpsideDownPosition;
        rb2D.AddForce(dir.normalized * 15f, ForceMode2D.Impulse);  
    }
    private void OnCollisionEnter2D(Collision2D other)
    {       
        if (other.gameObject.TryGetComponent<IDamagable>(out IDamagable damagable))
        {           
            damagable.Die();            
        }        
    }  
    private void GroundCheck()
    {
        grounded = false; 
        Vector2 centerPos = transform.position;
        RaycastHit2D hit;
        hit = Physics2D.Raycast(centerPos, Vector2.down, (col.bounds.size.y) / 2f + 0.1f);
        if (hit)
            Debug.DrawLine(centerPos, hit.point, Color.red, 0.1f);

        if (hit)        
            grounded = true;        
    }
}
