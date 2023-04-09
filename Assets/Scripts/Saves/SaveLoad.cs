using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public class SaveLoad 
{
    public static List<Game> SavedGames = new List<Game>();
    public static int SavedGamesCount => SavedGames.Count;

    public static void Save() 
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SavedGames.zao");
        bf.Serialize(file, SaveLoad.SavedGames);
        file.Close();
    }

    public static void Load() 
    {
        if (File.Exists(Application.persistentDataPath + "/SavedGames.zao")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SavedGames.zao", FileMode.Open);
            SaveLoad.SavedGames = (List<Game>)bf.Deserialize(file);
            file.Close();
        }
    }

    public static void CreateNewSave()
    {
        Load();

        Game g = new Game();
        SavedGames.Add(g);
        Game.current = g;

        Save();
    }

    public static void LoadSave(int n)
    {
        Load();

        Game.current = SavedGames[n];
    }

    public static void LoadLastSave()
    {
        LoadSave(SavedGamesCount - 1);
    }
}
