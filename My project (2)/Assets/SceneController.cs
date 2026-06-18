using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void OpenSettings()
    {
        SceneManager.LoadScene("audio_scene");
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("main_menu");
    }
}
