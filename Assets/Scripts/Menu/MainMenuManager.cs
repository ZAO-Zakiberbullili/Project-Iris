using System;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    private void Start() 
    {
        SaveManager.Instance.Load();
    }

    public void Continue()
    {
        SaveManager.Instance.PickMostRecentSave();

        SceneManager.LoadScene("Prologue");
    }

    public void NewGame()
    {
        SaveManager.Instance.CreateSave();
        
        SceneManager.LoadScene("Prologue");
    }

    public void Quit()
    {
        SaveManager.Instance.Save();

        Application.Quit();

#if UNITY_EDITOR
        string filePath = Path.Combine(Application.persistentDataPath, "Saves.zao");
        File.Delete(filePath);
        EditorApplication.ExitPlaymode();
#endif
    }
}
