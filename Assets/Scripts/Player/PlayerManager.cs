using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Manager")]
    [SerializeField] ShootWeapon firingControls;
    [SerializeField] FPSController rotationControls;

    [Header("Variables")]
    [SerializeField] float bulletSpeed; 
    [SerializeField] float turretRotationSpeed;
    [SerializeField] float maxTurretAngle;
    [SerializeField] float minTurretAngle;
    [Range(0, 1)]
    [SerializeField] float cameraSensitivity;

    // Start is called before the first frame update
    void Start()
    {
        firingControls.SetFiringSpeed(bulletSpeed);
        rotationControls.SetValues(turretRotationSpeed, maxTurretAngle, minTurretAngle, cameraSensitivity);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
