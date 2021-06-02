using System;
using System.Collections.Generic;

public class ModelsManager
{
    private static ModelsManager instance;

    private ScoreModel scoreModel;
    private FileLoaderModel loaderModel;

    public ScoreModel ScoreModel { get => scoreModel; set => scoreModel = value; }
    public FileLoaderModel LoaderModel { get => loaderModel; set => loaderModel = value; }

    public static ModelsManager getInstance()
    {
        if (instance == null)
            instance = new ModelsManager();
        return instance;
    }
}
