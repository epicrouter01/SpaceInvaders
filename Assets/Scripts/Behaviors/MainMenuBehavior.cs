using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehavior : MonoBehaviour
{
    public void play()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void exit()
    {
        Application.Quit();
    }
}
