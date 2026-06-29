using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    // --- SINGLETON PATTERN ---
    // This allows any other script (like your character spawner) to easily find the camera
    // by simply writing: MouseLook.Instance.SetTarget(newAvatar);
    public static MouseLook Instance { get; private set; }

    [Header("Target to Follow")]
    [Tooltip("Drag a player avatar here, OR leave it empty and the script will automatically find any active object tagged 'Player'!")]
    public Transform target;

    [Header("ARPG Camera Settings")]
    [Tooltip("How high above the player the camera should sit.")]
    public float height = 12f;

    [Tooltip("How far behind the player the camera should sit.")]
    public float distance = 10f;

    [Tooltip("The pitch angle (looking down). 45 to 60 degrees is perfect for ARPGs.")]
    public float pitchAngle = 55f;

    [Tooltip("How fast the camera catches up to the player. Higher = faster, Lower = smoother.")]
    public float smoothSpeed = 8f;

    [Header("Optional Mouse Orbit")]
    [Tooltip("Should the player be able to rotate the camera around the avatar?")]
    public bool allowRotation = true;

    [Tooltip("How fast the camera rotates around the player when dragging.")]
    public float rotationSensitivity = 120f;

    private float currentYaw = 0f;

    void Awake()
    {
        // Set up the static instance so other scripts can easily talk to this camera
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        // --- FAILSAFE 1: THE AUTOMATIC DETACH ---
        // If the camera is a child of the player, dynamically rip it out at runtime.
        // This completely prevents the recursive "spastic parenting" feedback loop!
        if (transform.parent != null)
        {
            transform.SetParent(null);
        }

        // --- FAILSAFE 2: THE SCALE RESET ---
        // Distorted scale causes extreme perspective shearing and warping.
        // We force it back to a perfect (1, 1, 1) scale!
        transform.localScale = Vector3.one;

        // --- FAILSAFE 3: PHYSICS DESTRUCTION ---
        // A camera should never have physics. If any exist, destroy them so the camera
        // cannot collide with the player or walls and get launched into space.
        if (TryGetComponent<Collider>(out Collider col))
        {
            Destroy(col);
        }
        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            Destroy(rb);
        }

        // Enable standard mouse cursor for ARPG gameplay
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Find our initial player target
        if (target == null)
        {
            FindActivePlayer();
        }
    }

    void Update()
    {
        // Rotate camera view left/right around the player by holding down the Right Mouse Button
        if (allowRotation && Input.GetMouseButton(1))
        {
            currentYaw += Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
        }
    }

    // LateUpdate runs after ALL movement and physics updates.
    void LateUpdate()
    {
        // If our target is missing/destroyed, try to dynamically locate the active player
        if (target == null)
        {
            FindActivePlayer();
            if (target == null) return; // If we still can't find one, try again next frame
        }

        // 1. Calculate the ideal target position based on rotation, distance, and height
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(pitchAngle, currentYaw, 0f);

        Vector3 targetPosition = target.position + rotation * dir;
        targetPosition.y = target.position.y + height; // Apply the elevation height

        // --- FAILSAFE 4: DUAL-MODE FRAME-RATE INDEPENDENT LERP ---
        float t;
        if (smoothSpeed <= 1f)
        {
            float clampedSpeed = Mathf.Clamp(smoothSpeed, 0.001f, 0.999f);
            t = 1f - Mathf.Pow(1f - clampedSpeed, Time.deltaTime * 60f); // Convert 0-1 lerp to 60fps independent
        }
        else
        {
            t = 1f - Mathf.Exp(-smoothSpeed * Time.deltaTime); // Use high-speed decay
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, t);

        // 3. Keep the camera focused directly on the player's torso
        transform.LookAt(target.position + Vector3.up * 1f);
    }

    /// <summary>
    /// Searches the scene for an active player, making sure it doesn't target itself.
    /// Only active (enabled) game objects tagged "Player" will be considered.
    /// </summary>
    private void FindActivePlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {
            // Verify the object is active in the hierarchy, and is not this camera
            if (p.activeInHierarchy && p != this.gameObject && p.transform != this.transform)
            {
                target = p.transform;
                break;
            }
        }
    }

    /// <summary>
    /// Explicitly tells the camera who to track. Any other script can call this!
    /// </summary>
    /// <param name="newTarget">The Transform of the newly spawned avatar.</param>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
