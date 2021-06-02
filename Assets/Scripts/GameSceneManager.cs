using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private GameField gameField = null;

    // Start is called before the first frame update
    void Start()
    {
        gameField.registerGameOverCallback(onGameOver);
    }

    private void onGameOver(int score)
    {
        updateHighscore(score);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
