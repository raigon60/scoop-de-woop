using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleSpawner : MonoBehaviour
{
    [Header("Prefabs to Spawn")]
    public GameObject ballPrefab;
    public GameObject featherPrefab;

    [Header("Spawn Settings")]
    public Transform spawnPoint;

    [Header("UI Reference")]
    [Tooltip("Drag the SpawningPopup Panel here!")]
    public GameObject uiPopup;

    private bool isPlayerNear = false;

    void Update()
    {
        // Only allow spawning if the player is standing inside the zone
        if (isPlayerNear)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Instantiate(ballPrefab, spawnPoint.position, spawnPoint.rotation);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Instantiate(featherPrefab, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // When the player walks up to the desk
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;

            if (uiPopup != null)
            {
                uiPopup.SetActive(true); // Show the popup!
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When the player walks away from the desk
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;

            if (uiPopup != null)
            {
                uiPopup.SetActive(false); // Hide the popup!
            }
        }
    }
}
