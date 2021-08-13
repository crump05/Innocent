using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    private SceneControl sceneControl;

    private void Awake()
    {
        sceneControl = GameObject.FindObjectOfType<SceneControl>();
    }
    public void PlayAgain()
    {
        StartCoroutine(sceneControl.SceneLoad(SceneManager.GetActiveScene().name));
    }
    public void MainMenu()
    {
        StartCoroutine(sceneControl.SceneLoad("Main Menu"));
    }
}
