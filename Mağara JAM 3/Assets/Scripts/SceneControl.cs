using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneControl : MonoBehaviour
{
    public Object[] scenes;

    private int level;

    public static SceneControl instance;

    private GameObject transition;

    public bool cutSceneOver;
    private void Awake()
    {
        if (instance == null)
            instance = this;

        else Destroy(instance);
        transition = GameObject.Find("Scene Transition");
        level = PlayerPrefs.GetInt("Level", 1);
        Debug.Log(level);

    }

    public IEnumerator SceneLoad(string levelName)
    {
        Debug.Log("wtf");
        Time.timeScale = 0f;
        transition.GetComponent<Animator>().SetTrigger("Outro");
        yield return new WaitForSecondsRealtime(1f);
        //   Time.timeScale = 1f;
        Cursor.visible = true;
        SceneManager.LoadScene(levelName);
    }
    public IEnumerator NextLevel()
    {
        Time.timeScale = 0f;
        transition.GetComponent<Animator>().SetTrigger("Outro");
        yield return new WaitForSecondsRealtime(1f);
        //   Time.timeScale = 1f;
        if (level + 1 <= scenes.Length)
        {
            level += 1;
            PlayerPrefs.SetInt("Level", level);
            Cursor.visible = true;
            SceneManager.LoadScene((level).ToString());

        }
        else
        {
            Cursor.visible = true;
            SceneManager.LoadScene("Outro");
        }

    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Outro" && cutSceneOver)
        {
            SceneManager.LoadScene("EndCredits");
        }
    }


}
