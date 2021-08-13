using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class CutsceneCanvas : MonoBehaviour
{
    public Sprite head;
    public TextMeshProUGUI text;

    public Sprite rachelHead;

    public Image source;

    public Sprite[] heads;
    public string[] names;
    private int index = 0;
    public string[] sentences;


    private GameObject player;

    private PlayerControl playerScript;
    private Animator anim;

    public bool sceneOver;

    

    public bool uselessButton;

    
    void Start()
    {
        Time.timeScale = 1f;
        PlayerPrefs.DeleteKey("Level");
        anim = GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name == "1" && GameObject.FindGameObjectWithTag("Player") != null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            player.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
            player.transform.GetChild(2).GetChild(3).gameObject.SetActive(true);
            source.sprite = rachelHead;
            playerScript = player.GetComponent<PlayerControl>();
        }
        if (SceneManager.GetActiveScene().name == "Intro")
        {
            StartCoroutine(Intro());
        }
        else if (SceneManager.GetActiveScene().name == "1")
        {
            source.sprite = rachelHead;
            StartCoroutine(Level1());
            playerScript.CantMove();
            playerScript.haloHead.SetActive(true);
            playerScript.haloVisual.SetActive(false);
        }
        if (SceneManager.GetActiveScene().name == "Outro")
        {
            StartCoroutine(Outro());
        }

    }

    private IEnumerator Outro()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(Type());
        yield return new WaitForSeconds(1f);
        NextSentence();
        source.sprite = heads[index];

        yield return new WaitForSeconds(3f);
        NextSentence();
        source.sprite = heads[index];
        yield return new WaitForSeconds(2f);
        NextSentence();
        source.sprite = heads[index];

        yield return new WaitForSeconds(1f);
        NextSentence();

        source.sprite = heads[index];
        yield return new WaitForSeconds(3f);
        NextSentence();

        source.sprite = heads[index];
        yield return new WaitForSeconds(3f);
        NextSentence();

        source.sprite = heads[index];
        yield return new WaitForSeconds(3f);
        NextSentence();

        source.sprite = heads[index];
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("EndCredits");
    }
    private IEnumerator Level1()
    {
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(Type());
        yield return new WaitForSeconds(2f);
        NextSentence();
        yield return new WaitForSeconds(2f);
        anim.SetTrigger("Out");
        yield return new WaitForSeconds(2f);
        playerScript.CanMove();
        transform.GetChild(0).gameObject.SetActive(false);
        playerScript.CanMove();
        playerScript.haloHead.SetActive(false);
        playerScript.haloVisual.SetActive(true);
    }
    private IEnumerator Intro()
    {
        yield return new WaitForSecondsRealtime(1f);
        StartCoroutine(Type());
        yield return new WaitForSeconds(3f);
        NextSentence();
        yield return new WaitForSeconds(2f);
        transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Scene").SetActive(false);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level", 1).ToString());
    }
    // Update is called once per frame
    void Update()
    {
     
        if (sceneOver)
        {
            GameObject.FindObjectOfType<SceneControl>().SceneLoad("Main Menu");
        }
    }
    private IEnumerator Type()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            text.text += letter;
            yield return new WaitForSeconds(0.02f);
        }
    }

    private void NextSentence()
    {
        if (index < sentences.Length - 1)
        {
            index++;
            text.text = "";
            StartCoroutine(Type());
        }
    }

}
