using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSoldier : MonoBehaviour
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

    public bool isIdle;

    private Rigidbody2D rb;

    private GameObject player;

    public bool isFollowing;

    public bool isInRange;

    private bool isWalking;

    public Animator anim;


    void Awake()
    {
        anim = GetComponent<Animator>();
        moveLeft = Random.Range(0, 2) > 0 ? true : false;
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        myScale = transform.localScale;
    }

    void Update()
    {
        anim.SetBool("isIdle",isIdle);
        HandleMovement();
        CheckForGround();
        if (isFollowing && !isIdle)
        {
            if (transform.position.x - player.transform.position.x > 0.1f)
            {
                moveLeft = true;
            }
            else if (player.transform.position.x - transform.position.x > 0.1f)
            {
                moveLeft = false;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }

        }
    }
    private void FixedUpdate()
    {


    }
    private void HandleMovement()
    {
        if (isIdle)
        {
            rb.velocity = Vector2.zero;
        }
        else
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
    }
    public void Seen()
    {

        isFollowing = true;
    }
    private void CheckForGround()
    {
        groundHit = Physics2D.Raycast(groundCheckPos.position, Vector2.down, 0.5f, groundLayer);
        wallHit = Physics2D.Raycast(groundCheckPos.position, Vector2.right * facingDirection, 0.1f, groundLayer);



        if ((!groundHit || wallHit) && !isIdle)
        {
            isFollowing = false;

            StartCoroutine(ChangeDirection());
        }




    }

    public IEnumerator ChangeDirection()
    {
        isIdle = true;
        yield return new WaitForSeconds(1f);
        isIdle = false;
        moveLeft = !moveLeft;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(Attack());
            isInRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isInRange = false;
        }
    }
    public IEnumerator Attack()
    {
        isIdle = true;
        anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.25f);
        if (isInRange)
        {
            player.GetComponent<PlayerControl>().GameOver();
        }
        isIdle = false;
    }
    
}
