using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;


public class SaveDataManager : MonoBehaviour
{
    private SaveData saveData = null;
    public static SaveDataManager instance { get; private set; }
    private FileSystemManager fileSystemManager;
    private List<IDataPersistance> dataList;

    private void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(this);
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
    }

    private void Start()
    {
        fileSystemManager = new FileSystemManager(Application.persistentDataPath);
        OnSceneLoad();
    }

    public void LoadGame()
    {
        saveData = fileSystemManager.QuickLoad();
        ForeachLoad(dataList);
    }

    public void OnSceneLoad()
    {
        dataList = FindAllIDataPersistanceExamplars();
        LoadGame();
    }

    public void SaveGame()
    {
        ForeachSave(dataList);
        fileSystemManager.Save(saveData, "quicksave");
    }


    public List<IDataPersistance> FindAllIDataPersistanceExamplars()
    {
        IEnumerable<IDataPersistance> dataPersistances = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<IDataPersistance>();
        return new List<IDataPersistance>(dataPersistances);
    }

    public void ForeachLoad(List<IDataPersistance> dataPersistances)
    {
        foreach (IDataPersistance dataPersistance in dataPersistances)
        {
            dataPersistance.LoadData(saveData);
        }

    }

    public void ForeachSave(List<IDataPersistance> dataPersistances)
    {
        foreach (IDataPersistance dataPersistance in dataPersistances)
        {
            dataPersistance.SaveData(ref saveData);
        }

    }
}
