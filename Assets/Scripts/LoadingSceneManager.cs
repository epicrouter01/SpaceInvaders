using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{    
    void Start()
    {
        registerModels();
        loadPersistentData();
        loadConfig();
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
        ModelsManager.getInstance().ConfigModel = new ConfigModel();
    }

    private void loadPersistentData()
    {
        PersistentData data;
        data = ModelsManager.getInstance().LoaderModel.getPersistentData();
        if (data == null) return;
        ModelsManager.getInstance().ScoreModel.Highscore = data.highscore;
    }
    
    private void loadConfig()
    {
        ConfigData data = ModelsManager.getInstance().LoaderModel.loadConfig();
        ModelsManager.getInstance().ConfigModel.Data = data;
    }
}