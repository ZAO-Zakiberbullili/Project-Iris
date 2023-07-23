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

    [SerializeField] private GameObject[] _deleteButtons;
    [SerializeField] private GameObject[] _confirmDeleteButtons;
    [SerializeField] private GameObject[] _cancelDeleteButtons;

    public TextMeshProUGUI[] loadButtonTexts = new TextMeshProUGUI[3];

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

    public void LoadGame()
    {
        _buttons.SetActive(false);
        _loadbuttons.SetActive(true);
        foreach (GameObject confirmButton in _confirmDeleteButtons)
        {
            confirmButton.SetActive(false);
        }
        foreach (GameObject cancelButton in _cancelDeleteButtons)
        {
            cancelButton.SetActive(false);
        }

        for (int n = 0; n < 3; n++)
        {
            TimeSpan playTime = SaveManager.Instance.GetPlayTime(n);
            DateTime saveTime = SaveManager.Instance.GetSaveTime(n);
            if (saveTime == DateTime.MinValue)
            {
                loadButtonTexts[n].text = "Create new save";

                _deleteButtons[n].SetActive(false);
            }
            else
            {
                // todo: also show player level, location image, location name
                loadButtonTexts[n].text = "Load game:" + "\n" + "Played for " + playTime.ToString() + ", saved at " + saveTime.ToString();
            }
        }
    }

    public void DeleteFirstSave() => DeleteSave(0);
    public void DeleteSecondSave() => DeleteSave(1);
    public void DeleteThirdSave() => DeleteSave(2);

    public void ConfirmFirstSaveDeletion() => ConfirmSaveDeletion(0);
    public void ConfirmSecondSaveDeletion() => ConfirmSaveDeletion(1);
    public void ConfirmThirdSaveDeletion() => ConfirmSaveDeletion(2);

    public void CancelFirstSaveDeletion() => CancelSaveDeletion(0);
    public void CancelSecondSaveDeletion() => CancelSaveDeletion(1);
    public void CancelThirdSaveDeletion() => CancelSaveDeletion(2);

    public void DeleteSave(int n)
    {
        _deleteButtons[n].SetActive(false);
        _confirmDeleteButtons[n].SetActive(true);
        _cancelDeleteButtons[n].SetActive(true);
    }

    public void ConfirmSaveDeletion(int n)
    {
        SaveManager.Instance.DeleteSave(n);

        SaveManager.Instance.Save();
        SaveManager.Instance.Load();

        loadButtonTexts[n].text = "Create new save";

        _confirmDeleteButtons[n].SetActive(false);
        _cancelDeleteButtons[n].SetActive(false);

        LoadGame();
    }

    public void CancelSaveDeletion(int n)
    {
        _deleteButtons[n].SetActive(true);
        _confirmDeleteButtons[n].SetActive(false);
        _cancelDeleteButtons[n].SetActive(false);
    }

    public void LoadFirstSave() => LoadSave(0);
    public void LoadSecondSave() => LoadSave(1);
    public void LoadThirdSave() => LoadSave(2);

    public void LoadSave(int n)
    {
        _loadbuttons.SetActive(false);
        _buttons.SetActive(true);

        if (loadButtonTexts[n].text == "Create new save")
        {
            NewGame();
        } 
        else
        {
            SaveManager.Instance.LoadSave(n);

            SceneManager.LoadScene("Prologue");
        }
    }

    public void BackToMainMenu()
    {
        _loadbuttons.SetActive(false);
        _buttons.SetActive(true);
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
