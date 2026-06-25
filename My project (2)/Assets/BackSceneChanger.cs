using UnityEngine;
using UnityEngine.SceneManagement;

public class BackSceneChanger : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("main_menu");
    }
}