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

    public void setXsense(float xSense)
    {
        xSensitivity = xSense;
    }

    public void setYsense(float ySense)
    {
        ySensitivity = ySense;
    }

    private void Update()
    {
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * xSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * ySensitivity;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

    }

}
