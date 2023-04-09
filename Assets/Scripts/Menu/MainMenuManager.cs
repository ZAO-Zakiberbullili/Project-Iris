#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _buttons;
    [SerializeField] private GameObject _loadbuttons;

    public void Continue()
    {
        SaveLoad.LoadLastSave();

        // for the demo only
        SceneManager.LoadScene("Prologue");

        // TODO: load last scene
    }

    public void NewGame()
    {
        SaveLoad.CreateNewSave();
        
        SceneManager.LoadScene("Prologue");
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

        // for the demo only
        SceneManager.LoadScene("Prologue");

        // TODO: load last scene
    }

    public void LoadGame2()
    {   
        _loadbuttons.SetActive(false);
        _buttons.SetActive(true);

        SaveLoad.LoadSave(1);

        // for the demo only
        SceneManager.LoadScene("Prologue");

        // TODO: load last scene
    }

    public void LoadGame3()
    {   
        _loadbuttons.SetActive(false);
        _buttons.SetActive(true);

        SaveLoad.LoadSave(2);

        // for the demo only
        SceneManager.LoadScene("Prologue");

        // TODO: load last scene
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
