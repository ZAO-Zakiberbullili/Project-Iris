#define PROLOGUE // to launch the game not from main menu
using UnityEngine;

public class InitPrologue : MonoBehaviour 
{
    public GameObject player;

    void Start()
    {
#if PROLOGUE
        SaveManager.Instance.Load();
        SaveManager.Instance.PickMostRecentSave();
#endif

        Instantiate(player, new Vector3(SaveManager.Instance.GetCurrentSaveData().player.x, SaveManager.Instance.GetCurrentSaveData().player.y, 0), Quaternion.identity);

        Time.timeScale = 1;

        GameStateController.Instance.ChangeGameState(GameState.Normal);
    }
}
