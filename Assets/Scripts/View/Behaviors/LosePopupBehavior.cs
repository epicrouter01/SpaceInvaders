using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LosePopupBehavior : MonoBehaviour
{
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
        transform.Find("Highscore").GetComponent<Text>().text = "Highscore: " + highscore;
        transform.Find("Score").GetComponent<Text>().text = "Score: " + score;
    }
}
