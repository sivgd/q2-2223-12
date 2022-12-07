using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camMove : MonoBehaviour
{
    public float xSensitivity;
    public float ySensitivity;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }

}
