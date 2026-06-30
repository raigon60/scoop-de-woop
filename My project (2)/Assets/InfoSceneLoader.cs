using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoSceneLoader : MonoBehaviour
{
    public void OpenInfoScene()
    {
        SceneManager.LoadScene("InfoScene");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("main_menu");
    }
}