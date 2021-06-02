using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private GameField gameField = null;
    [SerializeField] private LosePopupBehavior losePopup = null;

    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        gameField.registerGameOverCallback(onGameOver);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && !isPaused)
        {
            isPaused = true;
            gameField.pauseGame();
            showPausePopup();
        }
    }

    private void showPausePopup()
    {
        losePopup.gameObject.SetActive(true);
        losePopup.setPauseState();
        losePopup.Highscore = ModelsManager.getInstance().ScoreModel.Highscore;
    }

    private void onGameOver(int score)
    {
        updateHighscore(score);
        showLosePopup(score);
    }

    private void showLosePopup(int score)
    {
        losePopup.gameObject.SetActive(true);
        losePopup.setGameOverStates();
        losePopup.Score = score;
        losePopup.Highscore = ModelsManager.getInstance().ScoreModel.Highscore;
    }

    public void exitGame()
    {
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }

    public void restartGame()
    {
        losePopup.gameObject.SetActive(false);
        if (isPaused)
            gameField.resumeGame();
        else
            gameField.restartGame();

        isPaused = false;
    }

    private void updateHighscore(int score)
    {
        if (score >= ModelsManager.getInstance().ScoreModel.Highscore)
        {
            ModelsManager.getInstance().ScoreModel.Highscore = score;
            savePersistentData();
        }
    }

    private void savePersistentData()
    {
        ModelsManager.getInstance().LoaderModel.savePersistentData(getPersistanceData());
    }

    private PersistentData getPersistanceData()
    {
        PersistentData data = new PersistentData();
        data.highscore = ModelsManager.getInstance().ScoreModel.Highscore;
        return data;
    }
}
