using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileLoaderModel: Model
{
    public FileLoaderModel(): base("FileLoader")
    {

    }

    public void savePersistentData(PersistentData data)
    {
        File.WriteAllText(Application.persistentDataPath + "/data.json", JsonUtility.ToJson(data));
    }

    public PersistentData getPersistentData()
    {
        string data = null;
        try
        {
            data = File.ReadAllText(Application.persistentDataPath + "/data.json");
        }
        catch(FileNotFoundException e)
        {
            Debug.Log(e);
        }
        return data == null ? null : JsonUtility.FromJson<PersistentData>(data);
    }
}
