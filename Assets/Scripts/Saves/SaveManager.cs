using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SaveManager
{
    public static List<SaveData> Saves = new List<SaveData>();
    public static int SavesCount => Saves.Count;

    public static void Save() 
    {
        string filePath = Path.Combine(Application.persistentDataPath, "Saves.zao");
        FileStream file = File.Create(filePath);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, SaveManager.Saves);
        file.Close();
    }

    public static void Load() 
    {
        string filePath = Path.Combine(Application.persistentDataPath, "Saves.zao");
        if (File.Exists(filePath)) {
            FileStream file = File.Open(Application.persistentDataPath + "/Saves.zao", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            SaveManager.Saves = (List<SaveData>)bf.Deserialize(file);
            file.Close();
        }
    }

    public static void CreateSave()
    {
        SaveData newSave = new SaveData();
        SaveData.data = newSave;
        Saves.Add(newSave);
    }

    public static DateTime GetSaveCreationTime(int n)
    {
        if (n < SavesCount)
        {
            return Saves[n].saveTime;
        }
        else 
        {
            return DateTime.MinValue;
        }
        ;
    }

    public static void LoadSave(int n)
    {
        SaveData.data = Saves[n];
    }
}
