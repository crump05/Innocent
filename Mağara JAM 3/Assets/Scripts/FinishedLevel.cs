using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedLevel : MonoBehaviour
{
    GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.playerWon = true;
        }
    }
}
