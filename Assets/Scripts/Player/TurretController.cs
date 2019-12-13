using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public bool isActive = false;

    [Header("Movement & Rotation")]
    private float rotSpeed = 90f;
    [Range(0.1f,1.0f)]
    private float sensitivity = 1f;
    private float xRotLowerClampRange = 30f;
    private float xRotUpperClampRange = 30f;
    private float xRot = 0f;

    [Header("Turret Parts")]
    [SerializeField] Transform Barrel;

    [Header("Transform Refs")]
    private PlayerCamera actingCamera;
    [SerializeField] Transform camRoot;

    [Header("Input Dependencies")]
    [SerializeField] JoystickInput newLeftStick;

    private void Start()
    {
        actingCamera = transform.GetChild(3).GetComponent<PlayerCamera>();
        camRoot = actingCamera.transform.GetChild(0);
        newLeftStick = LeftJoystickPass.instance.LeftJoystickLocator();
    }

    private void Update()
    {
        if (isActive && LockOnButton.instance.isLockedOn == false)
        {
            RotateTurret();
        }
        else if (isActive && LockOnButton.instance.isLockedOn == true)
        {
            transform.LookAt(LockOnButton.instance.returnLockOnTarget().transform);
        }
    }

    public void SetValues(float newRotateSpeed, float newUpperClampRange, float newLowerClampRange, float newSensitivity)
    {
        rotSpeed = newRotateSpeed;
        xRotUpperClampRange = newUpperClampRange;
        xRotLowerClampRange = newLowerClampRange;
        sensitivity = newSensitivity;
    }

    private void RotateTurret()
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
    }

    public void ActivateTurret()
    {
        isActive = true;
    }

    public void DeactivateTurret()
    {
        isActive = false;
    }

    public PlayerCamera ReturnActingCam()
    {
        return actingCamera;
    }
}
