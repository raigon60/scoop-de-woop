using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnNet : MonoBehaviour
{
    [Tooltip("Drag your RespawnPoint object here!")]
    public Transform safeLocation;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            other.transform.position = safeLocation.position;


            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
            }
        }

        else if (other.GetComponent<Rigidbody>() != null)
        {
            Destroy(other.gameObject);
        }
    }
}
