using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public float moveSpeed;

    private Vector2 myPos;
    private Vector2 myScale;

    private bool moveLeft;

    public LayerMask groundLayer;

    public LayerMask playerLayer;


    public Transform groundCheckPos;

    private RaycastHit2D groundHit;

    private RaycastHit2D wallHit;

    private int facingDirection;



    private Rigidbody2D rb;

    private GameObject player;





    void Awake()
    {
        moveLeft = Random.Range(0, 2) > 0 ? true : false;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        myScale = transform.localScale;
    }

    void Update()
    {
        HandleMovement();
        CheckForGround();

    }
    private void FixedUpdate()
    {


    }
    private void HandleMovement()
    {
  
    
            if (moveLeft)
            {
                facingDirection = -1;
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                transform.localScale = new Vector2(-myScale.x, myScale.y);
            }
            else
            {
                facingDirection = 1;
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                transform.localScale = new Vector2(myScale.x, myScale.y);
            }
        
    }

    private void CheckForGround()
    {
        groundHit = Physics2D.Raycast(groundCheckPos.position, Vector2.down, 0.5f, groundLayer);
        wallHit = Physics2D.Raycast(groundCheckPos.position, Vector2.right * facingDirection, 0.1f, groundLayer);
        if (!groundHit || wallHit)
        {


            moveLeft = !moveLeft;
        }




    }

 

}
