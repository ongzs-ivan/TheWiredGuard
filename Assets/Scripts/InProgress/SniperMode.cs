using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperMode : MonoBehaviour
{
    public GameObject playerCam;
    public GameObject scopeCam;
    public bool activated = false;

    private void ScopeMode()
    {
        if (activated)
        {
            playerCam.GetComponent<Camera>().fieldOfView = 30;
            scopeCam.SetActive(true);
        }
        else if (!activated)
        {
            playerCam.GetComponent<Camera>().fieldOfView = 60;
            scopeCam.SetActive(false);
        }
    }
}
