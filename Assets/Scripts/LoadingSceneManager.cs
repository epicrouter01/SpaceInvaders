using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    private PersistentData data;
    
    void Start()
    {
        registerModels();
        loadPersistentData();
        setData();
        StartCoroutine(loadMenuWithDelay());
    }

    IEnumerator loadMenuWithDelay()
    {
        yield return new WaitForSeconds(2);
        loadMenu();
    }

    private void loadMenu()
    {
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }

    private void registerModels()
    {
        ModelsManager.getInstance().ScoreModel = new ScoreModel();
        ModelsManager.getInstance().LoaderModel = new FileLoaderModel();
    }

    private void loadPersistentData()
    {
        data = ModelsManager.getInstance().LoaderModel.getPersistentData();
    }

    private void setData()
    {
        if (data == null) return;

        ModelsManager.getInstance().ScoreModel.Highscore = data.highscore;
    }
}