using System;
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

    public void Pause()
    {
        Time.timeScale = 0f;
        _pauseMenu.SetActive(true);
        _pauseButton.SetActive(false);

        SaveGame();
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        _pauseMenu.SetActive(false);
        _pauseButton.SetActive(true);
    }

    public void SaveGame()
    {
        SaveManager.Instance.GetCurrentSaveData().playTime += (DateTime.Now - SaveManager.Instance.GetCurrentSaveData().saveTime);
        SaveManager.Instance.GetCurrentSaveData().saveTime = DateTime.Now;
        SaveManager.Instance.Save();

        // TODO: add some effect or text to show that game is saved
    }

    public void Quit()
    {
        SaveGame();

        Application.Quit();

#if UNITY_EDITOR
        File.Delete(Application.persistentDataPath + "/SavedGames.zao");
        EditorApplication.ExitPlaymode();
#endif
    }
}
