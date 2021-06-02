using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LosePopupBehavior : MonoBehaviour
{
    [SerializeField] private GameObject highscoreField = null;
    [SerializeField] private GameObject scoreField = null;
    [SerializeField] private GameObject playTextField = null;
    [SerializeField] private GameObject titleField = null;

    private int score;
    private int highscore;

    public int Score
    {
        get => score;
        set
        {
            score = value;
            updateUI();
        }
    }
    public int Highscore
    {
        get => highscore;
        set
        {
            highscore = value;
            updateUI();
        }
    }

    private void updateUI()
    {
        highscoreField.GetComponent<Text>().text = "Highscore: " + highscore;
        scoreField.GetComponent<Text>().text = "Score: " + score;
    }

    public void setPauseState()
    {
        playTextField.GetComponent<Text>().text = "Resume";
        titleField.GetComponent<Text>().text = "Pause";
        scoreField.gameObject.SetActive(false);
    }

    public void setGameOverStates()
    {
        playTextField.GetComponent<Text>().text = "Restart";
        titleField.GetComponent<Text>().text = "Game Over";
        scoreField.gameObject.SetActive(true);
    }
}
