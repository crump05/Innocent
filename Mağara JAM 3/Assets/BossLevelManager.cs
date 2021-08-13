using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevelManager : MonoBehaviour
{

    public bool cutsceneEnded = false;

    public float bossHealth;

    public bool bossDied;


    private PlayerControl player;
    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerControl>();
        StartCoroutine(BossIntro());
    }


    void Update()
    {

    }


    private IEnumerator BossIntro()
    {
        player.CantMove();
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(3f);
        Time.timeScale = 1f;
        cutsceneEnded = true;
        player.CanMove();
    
    }
}
