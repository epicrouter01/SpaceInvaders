using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreChangerBehavior : MonoBehaviour
{

    private ScoreStrategy changeScoreStrategy = new ScoreStrategy();
    private int score = 0;

    public int Score { get => score;}

    private void Start()
    {
        updateUI();
    }

    public void onEnemiesDestroyed(int count)
    {
        score += changeScoreStrategy.getScore(count);
        updateUI();
    }
    
    public void onGameStarted()
    {
        score = 0;
        updateUI();
    }

    private void updateUI()
    {
        GetComponent<Text>().text = "Score: " + Score;
    }
}
