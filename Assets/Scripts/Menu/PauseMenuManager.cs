#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.IO;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _pauseButton;

    [SerializeField] private GameObject _buttons;
    [SerializeField] private GameObject _loadbuttons;

    public void Pause()
    {
        Time.timeScale = 0f;
        _pauseMenu.SetActive(true);
        _pauseButton.SetActive(false);
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        _pauseMenu.SetActive(false);
        _pauseButton.SetActive(true);
    }

    public void SaveGame()
    {
        SaveLoad.Save();

        // TODO: add some effect or text to show that game is saved
    }

    public void LoadGame()
    {
        _buttons.SetActive(false);
        _loadbuttons.SetActive(true);
    }

    // TODO: handle errors if saves not created
    // TODO: show if saves not created

    public void LoadGame1()
    {   
        _loadbuttons.SetActive(false);
        _buttons.SetActive(true);

        SaveLoad.LoadSave(0);
    }

    public void LoadGame2()
    {   
        _loadbuttons.SetActive(false);
        _buttons.SetActive(true);

        SaveLoad.LoadSave(1);
    }

    public void LoadGame3()
    {   
        _loadbuttons.SetActive(false);
        _buttons.SetActive(true);

        SaveLoad.LoadSave(2);
    }

    public void ExitToMainMenu()
    {
        SaveLoad.Save();

        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        SaveLoad.Save();

        Application.Quit();

#if UNITY_EDITOR
        File.Delete(Application.persistentDataPath + "/SavedGames.zao");
        EditorApplication.ExitPlaymode();
#endif
    }
}
