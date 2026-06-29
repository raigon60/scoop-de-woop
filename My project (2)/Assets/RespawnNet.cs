using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnNet : MonoBehaviour
{
    [Tooltip("Drag your RespawnPoint object here!")]
    public Transform safeLocation;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object falling into the net is the Player
        if (other.CompareTag("Player"))
        {
            // 1. Teleport the player back to safety
            other.transform.position = safeLocation.position;

            // 2. Erase their falling momentum so they don't smash into the floor
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }
        }

        // Optional: Destroy any physics blocks (like crates) that fall off the map
        // so they don't lag the game by falling forever!
        else if (other.GetComponent<Rigidbody>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}
