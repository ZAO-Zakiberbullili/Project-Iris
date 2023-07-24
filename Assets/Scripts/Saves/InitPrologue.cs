using UnityEngine;

public class InitPrologue : MonoBehaviour 
{
    public GameObject player;

    void Start()
    {
        Instantiate(player, new Vector3(SaveManager.Instance.GetCurrentSaveData().player.x, SaveManager.Instance.GetCurrentSaveData().player.y, 0), Quaternion.identity);

        Time.timeScale = 1;

        GameStateController.Instance.ChangeGameState(GameState.Normal);
    }
}
