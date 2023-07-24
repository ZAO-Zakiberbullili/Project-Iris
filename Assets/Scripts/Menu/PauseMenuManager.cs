#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _pauseButton;

    public void TogglePauseContinue()
    {
        if (LoadGameManager.loadMenuActive == false)
        {
            if (GameStateController.Instance.CurrentState == GameState.Pause)
            {
                Continue();
            }
            else 
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        GameStateController.Instance.ChangeGameState(GameState.Pause);
        Time.timeScale = 0f;
        _pauseMenu.SetActive(true);
        _pauseButton.SetActive(false);
    }

    public void Continue()
    {
        GameStateController.Instance.ChangeGameState(GameState.Normal);
        Time.timeScale = 1f;
        _pauseMenu.SetActive(false);
        _pauseButton.SetActive(true);
    }

    public void SaveGame()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().SavePlayerPosition();
        SaveManager.Instance.GetCurrentSaveData().UpdatePlayTime();
        SaveManager.Instance.Save();
    }

    public void Quit()
    {
        SaveGame();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
