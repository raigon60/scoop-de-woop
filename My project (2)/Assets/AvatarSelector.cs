using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 
using UnityEngine.SceneManagement; 

public class AvatarSelector : MonoBehaviour
{
    [Header("Scene Management")]
    [Tooltip("Type the exact name of your gameplay lab scene!")]
    public string gameplaySceneName = "cosmicretrosquid";

    [Tooltip("Type the exact name of your main menu scene!")]
    public string mainMenuSceneName = "main_menu";

    [Header("Panels")]
    [Tooltip("Drag your MainMenuPanel from the Hierarchy here!")]
    public GameObject mainMenuPanel;

    [Tooltip("Drag your AvatarSelectionPanel from the Hierarchy here!")]
    public GameObject avatarSelectionPanel;

    [Header("Avatars")]
    [Tooltip("Drag your 5 animal objects from the Hierarchy into this list (ONLY needed in the lab scene)!")]
    public GameObject[] avatars;


    private float selectionAllowedTime = 0f;
    private float selectCooldownDelay = 0.5f; 

    private void Start()
    {
        if (avatars != null && avatars.Length > 0)
        {
            int selectedIndex = PlayerPrefs.GetInt("SelectedAvatar", 0);
            Debug.Log($"[AvatarSelector] Gameplay scene loaded! Activating saved avatar index: {selectedIndex}");

            for (int i = 0; i < avatars.Length; i++)
            {
                if (avatars[i] != null)
                {
                    avatars[i].SetActive(i == selectedIndex);
                }
            }
        }
    }

    public void OpenCharacterSelect()
    {
        Debug.Log("[AvatarSelector] OpenCharacterSelect called. Swapping panels.");

        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (avatarSelectionPanel != null) avatarSelectionPanel.SetActive(true);

        selectionAllowedTime = Time.unscaledTime + selectCooldownDelay;


        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    public void ChooseAvatar(int avatarIndex)
    {
        float currentTime = Time.unscaledTime;

        if (currentTime < selectionAllowedTime)
        {
            Debug.LogWarning($"[AvatarSelector] Blocked premature selection for avatar index {avatarIndex}. Cooldown active for another {selectionAllowedTime - currentTime:F2} seconds.");
            return;
        }

        Debug.Log($"[AvatarSelector] Selected avatar index {avatarIndex}. Saving choice and loading game...");

        PlayerPrefs.SetInt("SelectedAvatar", avatarIndex);
        PlayerPrefs.Save();

        SceneManager.LoadScene(gameplaySceneName);
    }

    public void ReturnToMenu()
    {
        Debug.Log("[AvatarSelector] Returning to the Main Menu...");

        GameObject[] persistentPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in persistentPlayers)
        {
            Destroy(player);
        }

        SceneManager.LoadScene(mainMenuSceneName);
    }
}