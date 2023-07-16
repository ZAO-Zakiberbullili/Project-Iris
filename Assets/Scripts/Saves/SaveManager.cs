using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SaveManager
{
    public List<SaveData> Saves = new List<SaveData>();
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
        // todo: if there are more than 3 saves 

        SaveData newSave = new SaveData();
        currentSaveData = newSave;
        Saves.Add(newSave);
    }

    public DateTime GetSaveCreationTime(int n)
    {
        if (n < Saves.Count)
        {
            return Saves[n].saveTime;
        }
        else
        {
            return DateTime.MinValue;
        }
    }

    public void LoadSave(int n)
    {
        currentSaveData = Saves[n];
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
