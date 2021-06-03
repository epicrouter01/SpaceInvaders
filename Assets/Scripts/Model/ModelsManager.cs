using System;
using System.Collections.Generic;

public class ModelsManager
{
    private static ModelsManager instance;

    private ScoreModel scoreModel;
    private FileLoaderModel loaderModel;
    private ConfigModel configModel;

    public ScoreModel ScoreModel { get => scoreModel; set => scoreModel = value; }
    public FileLoaderModel LoaderModel { get => loaderModel; set => loaderModel = value; }
    public ConfigModel ConfigModel { get => configModel; set => configModel = value; }

    public static ModelsManager getInstance()
    {
        if (instance == null)
            instance = new ModelsManager();
        return instance;
    }
}
