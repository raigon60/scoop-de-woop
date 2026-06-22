using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Required for changing scenes!

public class Teleporter : MonoBehaviour
{
    public string sceneToLoad = "PlanetRoom"; // Type the exact name of your next scene here

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object stepping on the pad is the Player
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
