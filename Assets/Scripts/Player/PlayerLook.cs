using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    float xRotation = 0f;

    float minimumX = -90.0f;
    float maximumX = 90.0f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp (xRotation, minimumX, maximumX);

        transform.localRotation = Quaternion.Euler (xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
