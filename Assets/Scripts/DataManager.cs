using System;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public string Name;
    public BestScoreData BestScore;

    /// <summary>
    /// Name of the file to save persistent information to.
    /// </summary>
    /// <remarks>
    /// The folder delimiter is prepended to the name as well.
    /// </remarks>
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
        LoadData();
    }

    /// <summary>
    /// Data to persist between scenes and sessions.
    /// </summary>
    [Serializable]
    class PersistentData
    {
        public string Name;
        public BestScoreData BestScore;
    }

    /// <summary>
    /// Used to persist the best or "high" score.
    /// </summary>
    /// <remarks>
    /// Stores name and score.
    /// </remarks>
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

    public void SaveData()
    {
        PersistentData data = new PersistentData();
        data.Name = Name;
        data.BestScore = BestScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + m_persistentFileName, json);
    }

    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + m_persistentFileName))
        {
            string json = File.ReadAllText(Application.persistentDataPath + m_persistentFileName);
            PersistentData data = JsonUtility.FromJson<PersistentData>(json);

            Name = data.Name;
            BestScore = data.BestScore;
        }
    }
}
