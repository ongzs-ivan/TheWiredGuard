using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Movement & Rotation")]
    private float rotSpeed = 90f;
    private float sensitivity = 1f;
    private float xRotLowerClampRange = 30f;
    private float xRotUpperClampRange = 30f;
    private float xRot = 0f;

    [Header("Turret Parts")]
    [SerializeField] Transform Barrel;
    //[SerializeField] Transform RightBarrel;

    [Header("Transform Refs")]
    [SerializeField] Transform camRoot;

    [Header("Input Dependencies")]
    [SerializeField] JoystickInput newLeftStick;

    private void Update()
    {
        RotatePlayer();
    }

    public void SetValues(float newRotateSpeed, float newUpperClampRange, float newLowerClampRange, float newSensitivity)
    {
        rotSpeed = newRotateSpeed;
        xRotUpperClampRange = newUpperClampRange;
        xRotLowerClampRange = newLowerClampRange;
        sensitivity = newSensitivity;
    }

    private void RotatePlayer()
    {
        // Rotate on Y axis
        transform.Rotate(0, newLeftStick.Horizontal * Time.deltaTime * rotSpeed * sensitivity, 0f);

        //// Rotate on X axis
        xRot += newLeftStick.Direction.y * Time.deltaTime * rotSpeed * sensitivity;
        xRot = Mathf.Clamp(xRot, -xRotLowerClampRange, xRotUpperClampRange);
        ElevateBarrel();
        
        camRoot.rotation = Quaternion.Euler(-xRot, transform.rotation.eulerAngles.y, 0);
    }

    private void ElevateBarrel()
    {
        Barrel.localRotation = Quaternion.Euler(-xRot, 0, 0);
        //RightBarrel.localRotation = Quaternion.Euler(0, 0, xRot);
    }
}
