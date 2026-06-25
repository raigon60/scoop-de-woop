using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit_button : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("main_menu");
    }
}