using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HunterScript : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float minimumDistance;
    public Vector2 hunterPosition;
    public Vector2 lastHunterPosition;
    public float hunterLastFramePositionX;
    public float checkPosTimer;
    public float showMe;
    public float hunterPositionX;
    public float lastHunterPositionX;
    public bool movingRight = true;
    public float checkDirectionTimer;
    public float dirCheck = -0.2f;
    public float obsticleCheck;
    Collider2D collider;


    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {        
        if (rb2D.position.x - hunterLastFramePositionX > -0.1)
        {
            movingRight = true;            
        }        
        else if(Mathf.Abs(rb2D.position.x - hunterLastFramePositionX) > dirCheck)
            movingRight = false;

        checkPosTimer += Time.deltaTime;
        checkDirectionTimer += Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        RaycastHit2D wallInfo = Physics2D.Raycast(rb2D.position, Vector2.right, (collider.bounds.size.x/2) + 0.1f);
        if (wallInfo == true)
        {
            GravityFlip();
            Debug.Log(wallInfo.transform.name);
        }
        
        //if (checkPosTimer >= 1)
        //{
        //    //obsticleCheck = rb2D.position.x - lastHunterPosition.x;
        //    //if (movingRight && obsticleCheck < 0.003)
        //    ////(Vector2.Distance(lastHunterPosition, rb2D.position) < 0.005f)            
        //    //    GravityFlip();            
        //    //else if (!movingRight && obsticleCheck < -0.1)
        //    //    GravityFlip();         

            //    checkPosTimer = 0;
        //}
        //else if (checkPosTimer >= 0.5)
        //{
        //    lastHunterPosition = rb2D.position;            
        //}
        if (checkDirectionTimer >= 0.1f)
        {
            hunterLastFramePositionX = rb2D.position.x;
            checkDirectionTimer = 0;
        }

    }

    private void GravityFlip()
    {
        rb2D.gravityScale *= -1;
        transform.up = transform.up * -1;
        //showMe = rb2D.position.x - lastHunterPosition.x;
        //Debug.Log(showMe);
    }
}
