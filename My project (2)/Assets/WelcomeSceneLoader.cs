using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeSceneLoader : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("main_menu");
    }
}
