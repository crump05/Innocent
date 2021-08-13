using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    SceneControl sceneControl;

    private void Awake()
    {
        sceneControl = GameObject.FindObjectOfType<SceneControl>();
    }
    public void PlayButton()
    {
        
        StartCoroutine(sceneControl.SceneLoad("Intro"));
    }


    public void QuitButton()
    {
        Application.Quit();
    }
}
