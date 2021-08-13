using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceRay : MonoBehaviour
{
    public float distance;
    private BoxCollider2D bc;
    GameObject mob;
    private void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        bc.size = new Vector2(distance * 2, 0.1f);


        mob = transform.parent.gameObject;
        gameObject.transform.SetParent(null);
        transform.position = mob.transform.position;
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (mob == null)
            {
                Destroy(this);
                return;
            }
            mob.GetComponent<ActiveSoldier>().Seen();
        }
    }

}
