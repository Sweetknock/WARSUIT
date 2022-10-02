using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] float keyboadInputSensitivity = 20f;
    [SerializeField] float mouseInputSensitivity = 0.1f;
    [SerializeField] bool continuous = true;
    [SerializeField] Transform bottomLeftBorder;
    [SerializeField] Transform topRightBorder;
    Vector3 input;
    Vector3 pointOfOrigin;


    private void Update()
    {
        NullInput();
        MoveCameraInput();
        MoveCamera();
    }

    private void NullInput()
    { 
        input.x = 0;
        input.y = 0;
        input.z = 0;
    }

    private void MoveCamera()
    {
        Vector3 position = transform.position;
        position += (input * Time.deltaTime);
        position.x = Mathf.Clamp(position.x, bottomLeftBorder.position.x, topRightBorder.position.x);
        position.z = Mathf.Clamp(position.z, bottomLeftBorder.position.z, topRightBorder.position.z);

        transform.position = position;
    }


    private void MoveCameraInput()
    {
        AxisInput();
        MouseInput();
    }

    private void MouseInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            pointOfOrigin = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 mouseInput = Input.mousePosition;
            input.x -= (mouseInput.x - pointOfOrigin.x) * mouseInputSensitivity;
            input.z -= (mouseInput.y - pointOfOrigin.y) * mouseInputSensitivity;
            if(continuous == false)
                pointOfOrigin = mouseInput;
        }
    }


    private void AxisInput()
    {
        input.x += Input.GetAxisRaw("Horizontal") * keyboadInputSensitivity;
        input.z += Input.GetAxisRaw("Vertical") * keyboadInputSensitivity;
    }
}
