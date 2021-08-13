using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HaloProjectile : MonoBehaviour
{
    Transform player;
    private Rigidbody2D rb;
    private Vector2 destination;
    public float distance;
    public bool move;
    private float timer;
    private bool coroutine;
    public bool moveTransform;
    private float moveTimer;
    private Animator anim;
    public GameObject slimeParticle;
    public GameObject demonParticle;

    private AudioSource source;
    public AudioClip haloSFX;

    public GameObject haloVisual;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
        haloVisual = transform.parent.GetChild(1).gameObject;
        rb = GetComponent<Rigidbody2D>();
        player = transform.root;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        if (move)
        {
            if (coroutine)
            {
                StartCoroutine(Throw());
                anim.SetBool("return", false);
            }
            else
            {
                anim.SetBool("return", true);
            }
            if (moveTransform && moveTimer <= 2.5f)
            {
                moveTimer += Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, player.position, distance * Time.deltaTime);
            }
            else if (moveTransform && moveTimer > 2.5f && moveTimer <= 5f)
            {
                moveTimer += Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, player.position, distance * distance * Time.deltaTime);
            }
            else if (moveTransform && moveTimer > 5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, distance * distance * distance * Time.deltaTime);
            }

        }



    }
    private void FixedUpdate()
    {
        if (move && !coroutine && timer < 0.5f)
        {
            timer += Time.fixedDeltaTime;
            rb.velocity = Vector2.MoveTowards(rb.velocity, (player.position - transform.position).normalized * distance, Time.deltaTime * distance * 4f);
        }
        else if (move && !coroutine && timer >= 0.5f)
        {
            timer = 0;
            moveTransform = true;
        }


    }
    public void RotateOnStart(float angle)
    {
     
        source.PlayOneShot(haloSFX);
        transform.position = haloVisual.transform.position;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        destination = -transform.up;
        move = true;
        coroutine = true;
    }
    IEnumerator Throw()
    {
        rb.velocity = destination * distance;
        yield return new WaitForSeconds(0.5f);
        coroutine = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == TagManager.PLAYER_TAG && !coroutine)
        {
            move = false;
            rb.velocity = Vector2.zero;
            haloVisual.SetActive(true);
            gameObject.SetActive(false);
            coroutine = true;
            moveTransform = false;
            timer = 0;
        }
        if (collision.gameObject.tag == TagManager.SLIME_TAG)
        {
            GameObject temp = Instantiate(slimeParticle, collision.transform.position, slimeParticle.transform.rotation);
            Destroy(collision.gameObject);
            Destroy(temp, 2);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject temp = Instantiate(demonParticle, collision.transform.position, slimeParticle.transform.rotation);
            Destroy(collision.gameObject);
            Destroy(temp, 2);

        }
    }

}
