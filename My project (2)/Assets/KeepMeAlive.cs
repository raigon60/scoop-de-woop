using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepMeAlive : MonoBehaviour
{
    // This creates a shared memory slot across the entire game to remember the "original" avatar
    private static KeepMeAlive originalInstance;

    void Awake()
    {
        // 1. Check if the memory slot is already full (meaning an original avatar exists)
        // AND make sure the original isn't the one running this code right now.
        if (originalInstance != null && originalInstance != this)
        {
            // We are a clone! Destroy ourselves immediately before anyone notices.
            Destroy(this.gameObject);
            return;
        }

        // 2. If the memory slot is empty, we must be the first one here! Claim the title of "Original".
        originalInstance = this;

        // 3. Tell Unity to protect the original.
        DontDestroyOnLoad(this.gameObject);
    }
}

