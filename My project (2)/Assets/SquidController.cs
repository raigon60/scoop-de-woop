using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidController : MonoBehaviour
{
    public float speed = 5f;      // How fast the squid moves
    private Animator anim;        // Reference to the Animator we just set up

    void Start()
    {
        // Tell the script to find the Animator on the squid
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Get input from WASD or Arrow Keys
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Create a movement vector based on input
        Vector3 movement = new Vector3(moveX, 0f, moveZ);

        // If the player is pressing a movement key...
        if (movement.magnitude > 0.1f)
        {
            // 1. Move the squid in the world
            transform.Translate(movement * speed * Time.deltaTime, Space.World);

            // 2. Make the squid face the direction it is moving
            transform.rotation = Quaternion.LookRotation(movement);

            // 3. Tell the Animator to play the Swim animation!
            anim.SetBool("isSwimming", true);
        }
        else
        {
            // If the player let go of the keys, stop swimming and go back to Idle
            anim.SetBool("isSwimming", false);
        }
    }
}
