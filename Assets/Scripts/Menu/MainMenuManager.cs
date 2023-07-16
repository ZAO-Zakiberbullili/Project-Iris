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
    [SerializeField] private GameObject _buttons;
    [SerializeField] private GameObject _loadbuttons;

    public TextMeshProUGUI[] loadButtonTexts = new TextMeshProUGUI[3];

    private void Start() 
    {
        SaveManager.Instance.Load();
    }

    public void Continue()
    {
        // TODO

        SceneManager.LoadScene("Prologue");
    }

    public void NewGame()
    {
        SaveManager.Instance.CreateSave();
        
        SceneManager.LoadScene("Prologue");
    }

    public void LoadGame()
    {
        _buttons.SetActive(false);
        _loadbuttons.SetActive(true);

        for (int n = 0; n < 3; n++)
        {
            DateTime saveCreationTime = SaveManager.Instance.GetSaveCreationTime(n);
            if (saveCreationTime == DateTime.MinValue)
            {
                loadButtonTexts[n].text = "Create new save";
            }
            else
            {
                loadButtonTexts[n].text = "Load game: save created at " + saveCreationTime.ToString();
            }
        }
    }

    public void LoadFirstSave()
    {   
        _loadbuttons.SetActive(false);
        _buttons.SetActive(true);

        SaveManager.Instance.LoadSave(0);

        SceneManager.LoadScene("Prologue");
    }

    public void LoadSecondSave()
    {   
        _loadbuttons.SetActive(false);
        _buttons.SetActive(true);

        SaveManager.Instance.LoadSave(1);

        SceneManager.LoadScene("Prologue");
    }

    public void LoadThirdSave()
    {   
        _loadbuttons.SetActive(false);
        _buttons.SetActive(true);

        SaveManager.Instance.LoadSave(2);

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
