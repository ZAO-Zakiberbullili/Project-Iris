using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
    public static string NextScene;
    public void Start()
    {
        SceneManager.LoadScene(NextScene);
    }
}
