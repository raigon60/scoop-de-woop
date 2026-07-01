using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Required for clearing selection focus!
using UnityEngine.SceneManagement; // Required for transitioning scenes!

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

    // Time-based lock using unscaled real-world seconds.
    // This completely bypasses coroutines, time-scaling, and frame delays.
    private float selectionAllowedTime = 0f;
    private float selectCooldownDelay = 0.5f; // 0.5 seconds of absolute lock-out time

    // This runs automatically when the scene starts
    private void Start()
    {
        // If we have avatars assigned (which means we are in the gameplay lab scene)
        if (avatars != null && avatars.Length > 0)
        {
            // Read the saved avatar index from the menu selection. Default to 0 (Snake) if none exists.
            int selectedIndex = PlayerPrefs.GetInt("SelectedAvatar", 0);
            Debug.Log($"[AvatarSelector] Gameplay scene loaded! Activating saved avatar index: {selectedIndex}");

            // Loop through all placed avatars and only activate the one that matches our saved choice
            for (int i = 0; i < avatars.Length; i++)
            {
                if (avatars[i] != null)
                {
                    avatars[i].SetActive(i == selectedIndex);
                }
            }
        }
    }

    // This handles swapping the panels and starting the safety cooldown
    public void OpenCharacterSelect()
    {
        Debug.Log("[AvatarSelector] OpenCharacterSelect called. Swapping panels.");

        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (avatarSelectionPanel != null) avatarSelectionPanel.SetActive(true);

        // 1. Lock out any character selection clicks for the next 0.5 seconds
        selectionAllowedTime = Time.unscaledTime + selectCooldownDelay;

        // 2. Clear Unity's EventSystem focus. 
        // This prevents Unity from auto-submitting/auto-clicking the first active button (SNAKE) 
        // in the newly opened panel due to lingering input.
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    // This method is called by your UI buttons. 
    public void ChooseAvatar(int avatarIndex)
    {
        float currentTime = Time.unscaledTime;

        // SAFETY GUARD: If the current real-world time is less than our allowed unlock time,
        // ignore the click completely!
        if (currentTime < selectionAllowedTime)
        {
            Debug.LogWarning($"[AvatarSelector] Blocked premature selection for avatar index {avatarIndex}. Cooldown active for another {selectionAllowedTime - currentTime:F2} seconds.");
            return;
        }

        Debug.Log($"[AvatarSelector] Selected avatar index {avatarIndex}. Saving choice and loading game...");

        // 1. Save the selected index persistently so it survives the scene transition
        PlayerPrefs.SetInt("SelectedAvatar", avatarIndex);
        PlayerPrefs.Save();

        // 2. Load the actual gameplay scene where the avatars are waiting!
        SceneManager.LoadScene(gameplaySceneName);
    }

    // NEW METHOD: Call this from your "Back to Menu" button in the lab scene
    public void ReturnToMenu()
    {
        Debug.Log("[AvatarSelector] Returning to the Main Menu...");

        // Fix: Because the avatar uses DontDestroyOnLoad, it can get disconnected from the 'avatars' list 
        // if you teleported to another planet and came back. 
        // The absolute safest way to clean up is to find anything tagged "Player" and destroy it!
        GameObject[] persistentPlayers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in persistentPlayers)
        {
            Destroy(player);
        }

        // Load the main menu scene
        SceneManager.LoadScene(mainMenuSceneName);
    }
}