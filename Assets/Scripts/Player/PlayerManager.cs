using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [Header("Manager")]
    [SerializeField] ShootWeapon firingControls;
    [SerializeField] TurretController currentActiveTurret;
    [SerializeField] PlayerCamera currentActiveCamera;

    [Header("Variables")]
    [SerializeField] float bulletSpeed; 
    [SerializeField] float turretRotationSpeed;
    [SerializeField] float maxTurretAngle;
    [SerializeField] float minTurretAngle;
    [Range(0.1f, 1.0f)]
    [SerializeField] float cameraSensitivity;
    [Range(0.1f, 1.0f)]
    [SerializeField] float zoomSensitivity;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        AdjustTurretSettings();
        currentActiveCamera.Activate();
    }
    

    public void AdjustTurretSettings()
    {
        firingControls.SetFiringSpeed(bulletSpeed);
        currentActiveTurret.SetValues(turretRotationSpeed, maxTurretAngle, minTurretAngle, cameraSensitivity);
        currentActiveCamera.SetZoomSensitivity(zoomSensitivity);
        SwitchMainCamera(currentActiveCamera);
    }

    public void SwitchMainCamera(PlayerCamera newActiveCamera)
    {
        if (newActiveCamera != null)
        {
            currentActiveCamera.Deactivate();
            currentActiveCamera = newActiveCamera;
            currentActiveCamera.Activate();
        }
    }

    public void SwitchActiveTurret(TurretController newActiveTurret)
    {
        currentActiveTurret.isActive = false;
        currentActiveTurret = newActiveTurret;
        currentActiveTurret.isActive = true;
    }

    public Camera GetCurrentCamera()
    {
        return currentActiveCamera.returnAssignedCam();
    }
    
}
