using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotation : MonoBehaviour
{
    private float xrot = 0;
    private float yrot = 0;

    void Update()
    {
        xrot += Input.GetAxis("Mouse Y") * 2;
        yrot += Input.GetAxis("Mouse X") * 2;
        transform.rotation = Quaternion.Euler(-xrot, yrot, 0);
    }
}
