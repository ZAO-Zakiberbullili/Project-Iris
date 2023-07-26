using System;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Localization; 
using UnityEngine.Localization.Settings; 
using TMPro;

public class LoadGameManager : MonoBehaviour
{
    [SerializeField] private GameObject _buttons;
    [SerializeField] private GameObject _loadbuttons;
    [SerializeField] private TextMeshProUGUI[] loadButtonTexts = new TextMeshProUGUI[3];
    [SerializeField] private GameObject[] _deleteButtons;
    [SerializeField] private GameObject[] _confirmDeleteButtons;
    [SerializeField] private GameObject[] _cancelDeleteButtons;

    [Header("Only for MainMenu")]
    [SerializeField] private GameObject _continueButton;

    public static bool loadMenuActive = false;

    public void LoadGame()
    { 
        SaveManager.Instance.Load();

        loadMenuActive = true;

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
                loadButtonTexts[n].text = LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Create new save").Result;

                _deleteButtons[n].SetActive(false);
            }
            else
            {
                // todo: also show player level, location image, location name
                loadButtonTexts[n].text = string.Format(
                    LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Load save").Result,
                    // localization doesn't process \n correctly
                    playTime.ToString(),
                    saveTime.ToString()
                );

                _deleteButtons[n].SetActive(true);
            }
        }
    }

#region DeleteSaves
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

        LoadGame();
    }

    public void CancelSaveDeletion(int n)
    {
        _deleteButtons[n].SetActive(true);
        _confirmDeleteButtons[n].SetActive(false);
        _cancelDeleteButtons[n].SetActive(false);
    }
#endregion

    public void LoadFirstSave() => LoadSave(0);
    public void LoadSecondSave() => LoadSave(1);
    public void LoadThirdSave() => LoadSave(2);

    public void LoadSave(int n)
    {
        loadMenuActive = false;

        _loadbuttons.SetActive(false);
        _buttons.SetActive(true);

        if (loadButtonTexts[n].text == LocalizationSettings.StringDatabase.GetLocalizedStringAsync("Create new save").Result)
        {
            SaveManager.Instance.CreateSave();
        } 
        else
        {
            SaveManager.Instance.LoadSave(n);
        }

        SceneReloader.NextScene = "Prologue";
        SceneManager.LoadScene("IntermediateScene");
    }

    public void BackToPreviousMenu()
    {
        loadMenuActive = false;

        _loadbuttons.SetActive(false);
        _buttons.SetActive(true);

        if (SaveManager.Instance.SavesCount == 0)
        {
            _continueButton?.SetActive(false);
        }
    }

    public void LoadMainMenu()
    {
        loadMenuActive = false;

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>().SavePlayerPosition();
        SaveManager.Instance.GetCurrentSaveData().UpdatePlayTime();
        SaveManager.Instance.Save();

        SceneManager.LoadScene("MainMenu");
    }
}