using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadSettings()
    {
        SceneManager.LoadScene("audio_scene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("main_menu");
    }
}