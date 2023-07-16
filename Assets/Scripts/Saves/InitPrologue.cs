using UnityEngine;

public class InitPrologue : MonoBehaviour 
{
    public GameObject player;

    void Start()
    {
        Instantiate(player, new Vector3(SaveData.data.player.x, SaveData.data.player.y, 0), Quaternion.identity);

        Time.timeScale = 1;
    }
}
