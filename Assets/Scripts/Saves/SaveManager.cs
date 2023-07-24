using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEditor;

public class SaveManager
{
    public List<SaveData> Saves = new List<SaveData>();
    public int SavesCount { get { return Saves.Count; } }
    private SaveData currentSaveData;

    public void Save()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "Saves.zao");
        FileStream file = File.Create(filePath);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, Saves);
        file.Close();
    }

    public void Load()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "Saves.zao");
        if (File.Exists(filePath))
        {
            FileStream file = File.Open(filePath, FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            Saves = (List<SaveData>)bf.Deserialize(file);
            file.Close();
        }
    }

    public void CreateSave()
    {
        SaveData newSave = new SaveData();
        currentSaveData = newSave;

        if (SavesCount < 3)
        {
            Saves.Add(newSave);
        }
        else
        {
            DateTime leastRecentsaveTime = Saves[0].saveTime;
            SaveData oldestSave = Saves[0];

            foreach (SaveData data in Saves)
            {
                if (leastRecentsaveTime > data.saveTime)
                {
                    leastRecentsaveTime = data.saveTime;
                    oldestSave = data;
                }
            }

            Saves.Remove(oldestSave);
            
            Saves.Add(newSave);
        }
    }

    public TimeSpan GetPlayTime(int n)
    {
        if (n < SavesCount)
        {
            return Saves[n].playTime;
        }
        else
        {
            return TimeSpan.Zero;
        }
    }

    public DateTime GetSaveTime(int n)
    {
        if (n < SavesCount)
        {
            return Saves[n].saveTime;
        }
        else
        {
            return DateTime.MinValue;
        }
    }

    public void PickMostRecentSave()
    {
        DateTime mostRecentsaveTime = DateTime.MinValue;

        foreach (SaveData data in Saves)
        {
            if (mostRecentsaveTime < data.saveTime)
            {
                mostRecentsaveTime = data.saveTime;
                currentSaveData = data;
            }
        }
    }

    public void LoadSave(int n)
    {
        currentSaveData = Saves[n];
    }

    public void DeleteSave(int n)
    {
        Saves.Remove(Saves[n]);
    }
    public SaveData GetCurrentSaveData()
    {
        return currentSaveData;
    }

    // Singleton pattern
    private static SaveManager instance;
    public static SaveManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SaveManager();
            }
            return instance;
        }
    }
}
