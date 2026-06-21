using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;

    private Animator anim;
    private Rigidbody rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // --- 1. MOVEMENT ---
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, 0f, moveZ);

        if (movement.magnitude > 0.1f)
        {
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
            transform.rotation = Quaternion.LookRotation(movement);

            // This is now looking for your Pudu's walking parameter!
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        // --- 2. PHYSICS JUMPING ---
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("JumpTrigger");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
