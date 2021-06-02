using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject score = null;

    private void Start()
    {
        setScore();
    }

    private void setScore()
    {
        score.GetComponent<Text>().text = "Highscore: " + ModelsManager.getInstance().ScoreModel.Highscore;
    }

    public void play()
    {
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void exit()
    {
        Application.Quit();
    }
}
