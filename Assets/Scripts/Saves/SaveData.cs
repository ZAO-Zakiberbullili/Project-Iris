using System;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    public Player player;
    public TimeSpan playTime;
    public DateTime saveTime;

    public SaveData()
    {
        player = new Player();
        playTime = TimeSpan.Zero;
        saveTime = DateTime.Now;
    }

    public void UpdatePlayTime()
    {
        playTime += (DateTime.Now - saveTime);
        saveTime = DateTime.Now;
    }
}
