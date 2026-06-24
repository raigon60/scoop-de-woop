using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ConsoleSpawner : MonoBehaviour
{
    [Header("Items to Spawn")]
    [Tooltip("Drag your Bouncy Soccer Ball prefab here")]
    public GameObject ballPrefab;

    [Tooltip("Drag your Droppable_Feather prefab here")]
    public GameObject featherPrefab;

    [Header("Spawn Location")]
    [Tooltip("Create an Empty GameObject where things should drop, and drag it here")]
    public Transform spawnPoint;

    // This keeps track of whether the player is close enough to use the table
    private bool isPlayerNear = false;


    // This runs the exact moment something walks into our invisible trigger box
    private void OnTriggerEnter(Collider other)
    {
        // Check if the thing that walked in is the Player
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player entered console zone! Press [1] for Ball or [2] for Feather.");

            // TIP: If you have a UI Text object, you can turn it ON here!
        }
    }

    // This runs the exact moment the player walks OUT of the invisible trigger box
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player left console zone.");

            // TIP: If you have a UI Text object, you can turn it OFF here!
        }
    }

    private void Update()
    {
        // Only listen for these buttons if the player is actually standing at the table
        if (isPlayerNear)
        {
            // If the player presses the '1' key (top row of keyboard)
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SpawnItem(ballPrefab);
            }

            // If the player presses the '2' key
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SpawnItem(featherPrefab);
            }
        }
    }

    private void SpawnItem(GameObject prefabToSpawn)
    {
        // Make sure we actually assigned a prefab and a spawn point in the Inspector to prevent errors
        if (prefabToSpawn != null && spawnPoint != null)
        {
            // Instantiate creates a brand new copy of the prefab at our chosen location
            Instantiate(prefabToSpawn, spawnPoint.position, spawnPoint.rotation);
            Debug.Log("Spawned: " + prefabToSpawn.name);
        }
        else
        {
            Debug.LogWarning("Cannot spawn! Make sure you dragged your prefabs and SpawnPoint into the script.");
        }
    }
}
