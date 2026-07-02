using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepMeAlive : MonoBehaviour
{
    private static KeepMeAlive originalInstance;

    void Awake()
    {

        if (originalInstance != null && originalInstance != this)
        {

            Destroy(this.gameObject);
            return;
        }

        originalInstance = this;

        DontDestroyOnLoad(this.gameObject);
    }
}

