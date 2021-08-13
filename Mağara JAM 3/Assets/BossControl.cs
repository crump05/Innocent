using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossControl : MonoBehaviour
{
    private int index;

    private Animator animator;

    private BossLevelManager boss;

    private PlayerControl player;

    private float scaleX;

    private int facingDirection = -1;

    public GameObject throwable;
    public float throwSpeed;

    public GameObject particleFX;

    public float bossHealth;

    public SceneControl scene;
    bool shot;
    private GameObject healthBar;

    public GameObject tempParticle;

    bool died;
    private void Awake()
    {
        healthBar = GameObject.Find("BossHealth").transform.GetChild(0).gameObject;
        scene = GameObject.FindObjectOfType<SceneControl>();
        scaleX = transform.localScale.x;
        player = GameObject.FindObjectOfType<PlayerControl>();
        boss = GameObject.FindObjectOfType<BossLevelManager>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (healthBar != null)
            healthBar.transform.localScale = Vector2.Lerp(healthBar.transform.localScale, new Vector2(bossHealth / 100, healthBar.transform.localScale.y), 5 * Time.deltaTime);
        if (animator.enabled == true)
            index = animator.GetInteger("State");
        if (bossHealth <= 0)
        {

            if (!died)
            {
                died = true;
                tempParticle = Instantiate(particleFX, transform.position, particleFX.transform.rotation);
                Destroy(tempParticle, 2f);
                animator.SetTrigger("Die");
                Destroy(gameObject, 1f);
                Destroy(healthBar.transform.parent.gameObject);
                StartCoroutine(scene.SceneLoad("Outro"));
   

            }
        }

        if (player.transform.position.x > transform.position.x)
        {
            facingDirection = 1;
            transform.localScale = new Vector2(-scaleX, transform.localScale.y);
        }
        else if (player.transform.position.x < transform.position.x)
        {
            facingDirection = -1;
            transform.localScale = new Vector2(scaleX, transform.localScale.y);
        }
        if (index == 0)
        {

        }
        else if (index == 1 && shot == false)
        {
            shot = true;
            GameObject temp = Instantiate(throwable, transform.position, throwable.transform.rotation);
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(throwSpeed * facingDirection, 0);
            Destroy(temp, 5);
        }
        else if (index == 2)
        {

        }
    }

    public void Randomize()
    {
        shot = false;

        if (animator.GetInteger("State") == 0 && boss.cutsceneEnded == true)
        {
            animator.SetInteger("State", Random.Range(1, 3));
        }
        else
        {
            animator.SetInteger("State", 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Halo")
        {
            bossHealth -= 5;
        }
    }
}
