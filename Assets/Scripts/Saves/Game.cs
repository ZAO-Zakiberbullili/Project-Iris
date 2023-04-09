using System.Collections;
using UnityEngine;

[System.Serializable]
public class Game 
{
    public static Game current;
    
    public Player player;

    public Game()
    {
        current = this;

        player = new Player();
    }
}