using System;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string Name;
    public BestScoreData BestScore;

    private string m_persistentFileName = "/persistentData.json";

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadName();
        LoadBestScore();
    }

    [Serializable]
    class PersistentData
    {
        public string Name;
        public BestScoreData BestScore;
    }

    [Serializable]
    public struct BestScoreData
    {
        public string Name;
        public int Score;

        public override string ToString()
        {
            if (Name == string.Empty)
            {
                return "No Best Score Yet!";
            }
            else
            {
                return $"Best Score : {Name} : {Score}";
            }
        }
    }

    public void SaveName()
    {
        PersistentData data = new PersistentData();
        data.Name = Name;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + m_persistentFileName, json);
    }

    public void LoadName()
    {
        if (File.Exists(Application.persistentDataPath + m_persistentFileName))
        {
            string json = File.ReadAllText(Application.persistentDataPath + m_persistentFileName);
            PersistentData data = JsonUtility.FromJson<PersistentData>(json);

            Name = data.Name;
        }
    }

    public void SaveBestScore()
    {
        PersistentData data = new PersistentData();
        data.BestScore = BestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + m_persistentFileName, json);
    }

    public void LoadBestScore()
    {
        if (File.Exists(Application.persistentDataPath + m_persistentFileName))
        {
            string json = File.ReadAllText(Application.persistentDataPath + m_persistentFileName);
            PersistentData data = JsonUtility.FromJson<PersistentData>(json);

            BestScore = data.BestScore;
        }
    }
}
