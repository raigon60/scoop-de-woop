using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    [Header("Gravity Settings")]
    [Tooltip("Earth is -9.81. The Moon is -1.62. Jupiter is -24.79.")]
    public float planetGravityY = -1.62f;

    void Start()
    {
        // Change the global gravity the moment this scene loads
        Physics.gravity = new Vector3(0, planetGravityY, 0);
    }
}