using UnityEngine;

public class InitPrologue : MonoBehaviour 
{
    public GameObject player;

    void Start()
    {
        Instantiate(player, new Vector3(Game.current.player.x, Game.current.player.y, 0), Quaternion.identity);

        Time.timeScale = 1;
    }
}
