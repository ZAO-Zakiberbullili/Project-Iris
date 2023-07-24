#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _continueButton;

    private void Start() 
    {
        SaveManager.Instance.Load();

        if (SaveManager.Instance.SavesCount > 0)
        {
            _continueButton.SetActive(true);
        }
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
        if (LoadGameManager.loadMenuActive == false)
        {
            SaveManager.Instance.Save();

#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else            
            Application.Quit();
#endif            
        }
    }
}
