using System;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public SaveData data;
    public Player player;
    public DateTime saveTime;

    public SaveData()
    {
        data = this;
        player = new Player();
        saveTime = DateTime.Now;
    }
}