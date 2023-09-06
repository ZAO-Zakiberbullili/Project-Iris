#define PROLOGUE // to launch the game not from main menu
using UnityEngine;

public class InitPrologue : MonoBehaviour 
{
    public GameObject player;

    public delegate void PlayerInitiatedDelegate();
    public static event PlayerInitiatedDelegate OnPlayerInitiated;

    void Start()
    {
#if PROLOGUE
        SaveManager.Instance.Load();
        SaveManager.Instance.PickMostRecentSave();
#endif

        GameObject clone = Instantiate(player, new Vector3(SaveManager.Instance.GetCurrentSaveData().player.x, SaveManager.Instance.GetCurrentSaveData().player.y, SaveManager.Instance.GetCurrentSaveData().player.z), Quaternion.identity);

        clone.GetComponent<PlayerMove>().FindTileLayer();

        OnPlayerInitiated();

        Time.timeScale = 1;

        GameStateController.Instance.ChangeGameState(GameState.Normal);
    }
}
