using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangingSpike : MonoBehaviour
{
    private Rigidbody2D rb;

    public LayerMask collisionLayer;

    private RaycastHit2D playerCast;

    private bool collided;

    public GameObject groundParticle;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }




    void Update()
    {
        CheckForPlayer();
    }

    void CheckForPlayer()
    {
        if (collided)
            return;
        playerCast = Physics2D.Raycast(transform.position, Vector2.down, 8.5f, collisionLayer);

        if (playerCast)
        {
            collided = true;
            rb.gravityScale = 1f;
            Destroy(gameObject, 10f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && collided == true)
        {
            GameObject temp = Instantiate(groundParticle, new Vector2(transform.position.x, transform.position.y - 0.5f), groundParticle.transform.rotation);
            Destroy(temp, 2f);
            Destroy(gameObject);
        }
    }
}
