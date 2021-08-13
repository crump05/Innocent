using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerControl : MonoBehaviour
{
    private Rigidbody2D rb;
    public Transform checkTransform;
    private BoxCollider2D bc;

    public float speed;
    public float jumpForce;
    public float glideSpeed;
    private float moveValue;
    private float scaleX;

    public LayerMask whatIsGround;

    private bool isGrounded;
    private bool isGliding;

    public AudioClip jump;
    private AudioSource source;

    public bool canMoveBool;

    private Arms haloArms;

    private GameObject gameOverMenu;

    private Animator anim;

    public GameManager manager;

    public GameObject haloVisual, haloHead;

    private bool dieBool;
    void Awake()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        haloArms = GameObject.FindObjectOfType<Arms>();
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        scaleX = transform.localScale.x;
        bc = GetComponent<BoxCollider2D>();
        gameOverMenu = GameObject.Find("GameOverMenu");
        gameOverMenu.SetActive(false);
        CanMove();
    }


    void Update()
    {
        if (!canMoveBool)
            return;
        GetInput();
        GroundCheck();
        anim.SetInteger("Speed", (int)Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS));
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("IsGliding", isGliding);
        anim.SetBool("isGrounded", isGrounded);
    }

    private void FixedUpdate()
    {
        if (!canMoveBool)
            return;
        Movement();
    }

    void GetInput()
    {
        moveValue = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.Space) && !isGrounded)
        {
            isGliding = true;
        }
        else
        {
            isGliding = false;
        }
    }
    void Movement()
    {
        if (moveValue != 0)
        {
            rb.velocity = new Vector2(moveValue * speed, rb.velocity.y);
            transform.localScale = new Vector2(scaleX * moveValue, transform.localScale.y);
        }
        else
        {

            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (rb.velocity.y < 0 && isGliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -glideSpeed, float.MaxValue));
        }



    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapBox(checkTransform.position, new Vector2(bc.bounds.size.x - 0.1f, 0.01f), 0, whatIsGround);
    }

    private void Jump()
    {
        if (isGrounded)
        {
            source.PlayOneShot(jump);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, float.MinValue, jumpForce));

        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spike" || collision.gameObject.tag == "Slime")
        {
            GameOver();
        }
    }
    public void CantMove()
    {
        canMoveBool = false;
        haloArms.canShoot = false;
    }
    public void CanMove()
    {
        canMoveBool = true;

        haloArms.canShoot = true;

    }

    public void GameOver()
    {
        if (!dieBool)
        {
            anim.SetTrigger("Died");
            dieBool = true;
            CantMove();
            gameOverMenu.SetActive(true);
            rb.velocity = new Vector2(0, rb.velocity.y);
            haloVisual.transform.parent.gameObject.SetActive(false);
            Cursor.visible = true;

        }

    }



}
