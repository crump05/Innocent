using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    HaloProjectile halo;

    PlayerControl player;

    SceneControl sceneControl;

    public bool startGame;

    public bool playerWon;

    public bool playerDied;

    private GameObject levelName;

    MusicManager musicManager;
    void Awake()
    {
        StartCoroutine(GameStarted());

        levelName = GameObject.Find("Level Name");

        musicManager = GameObject.FindObjectOfType<MusicManager>();

        sceneControl = GameObject.FindObjectOfType<SceneControl>();
    }

    void Update()
    {
        #region Finished Level
        if (playerWon)
        {
            StartCoroutine(sceneControl.NextLevel());
            playerWon = false;
        }
        #endregion
    }

    public IEnumerator GameStarted()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
 


    }
}
