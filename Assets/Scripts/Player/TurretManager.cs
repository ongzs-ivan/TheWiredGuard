using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretManager : MonoBehaviour
{
    public static TurretManager instance;

    [SerializeField] private List<TurretController> turretList = new List<TurretController>();

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
        foreach (Transform child in transform)
        {
            TurretController turret = child.GetChild(1).GetComponent<TurretController>();
            turretList.Add(turret);
        }
    }

    public TurretController GetStartingTurret(int index)
    {
        return turretList[index];
    }

    public PlayerCamera GetPlayerCamera(int index)
    {
        return turretList[index].ReturnActingCam();
    }
}
