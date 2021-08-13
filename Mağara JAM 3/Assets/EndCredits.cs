using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCredits : MonoBehaviour
{
    public bool cutSceneOver;

    private void Update()
    {
        if (cutSceneOver)
        {
            Application.Quit();
        }
    }
}
