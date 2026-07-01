using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardSign : MonoBehaviour
{
    void LateUpdate()
    {
        // Make the sign look perfectly flush with the main camera every frame
        if (Camera.main != null)
        {
            transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
                             Camera.main.transform.rotation * Vector3.up);
        }
    }
}
