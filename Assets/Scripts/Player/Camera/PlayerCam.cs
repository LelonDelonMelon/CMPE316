using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

    float xRotation;
    float yRotation;
    public Transform orientation;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;       
        Cursor.visible = false;
    }
    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime* sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -20f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);


        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public float getXRotation()
    {
        return this.xRotation;
    }
}
