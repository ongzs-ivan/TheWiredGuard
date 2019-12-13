using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    [Header("Manager")]
    [SerializeField] ShootWeapon firingControls;
    [SerializeField] TurretManager turretManager;
    [SerializeField] TurretController currentActiveTurret;
    [SerializeField] PlayerCamera currentActiveCamera;

    [Header("Variables")]
    [SerializeField] float bulletSpeed; 
    [SerializeField] float turretRotationSpeed;
    [SerializeField] float maxTurretAngle;
    [SerializeField] float minTurretAngle;
    [Range(0.1f, 1.0f)]
    [SerializeField] float cameraSensitivity = 0.5f;
    [Range(0.01f, 1.0f)]
    [SerializeField] float zoomSensitivity = 0.01f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        //DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        currentActiveTurret = turretManager.GetStartingTurret(3);
        currentActiveCamera = turretManager.GetPlayerCamera(3);
        AdjustTurretSettings();
        currentActiveCamera.Activate();
        currentActiveTurret.ActivateTurret();
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
