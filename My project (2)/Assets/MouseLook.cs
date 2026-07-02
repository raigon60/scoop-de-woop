using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

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

        if (transform.parent != null)
        {
            transform.SetParent(null);
        }


        transform.localScale = Vector3.one;


        if (TryGetComponent<Collider>(out Collider col))
        {
            Destroy(col);
        }
        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            Destroy(rb);
        }


        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        if (target == null)
        {
            FindActivePlayer();
        }
    }

    void Update()
    {

        if (allowRotation && Input.GetMouseButton(1))
        {
            currentYaw += Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
        }
    }


    void LateUpdate()
    {

        if (target == null)
        {
            FindActivePlayer();
            if (target == null) return; 
        }

        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(pitchAngle, currentYaw, 0f);

        Vector3 targetPosition = target.position + rotation * dir;
        targetPosition.y = target.position.y + height; 


        float t;
        if (smoothSpeed <= 1f)
        {
            float clampedSpeed = Mathf.Clamp(smoothSpeed, 0.001f, 0.999f);
            t = 1f - Mathf.Pow(1f - clampedSpeed, Time.deltaTime * 60f); 
        }
        else
        {
            t = 1f - Mathf.Exp(-smoothSpeed * Time.deltaTime); 
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, t);


        transform.LookAt(target.position + Vector3.up * 1f);
    }


    private void FindActivePlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject p in players)
        {

            if (p.activeInHierarchy && p != this.gameObject && p.transform != this.transform)
            {
                target = p.transform;
                break;
            }
        }
    }


    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
