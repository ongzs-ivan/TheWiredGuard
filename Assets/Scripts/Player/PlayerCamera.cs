using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float zoomOutMin = 2;
    public float zoomOutMax = 60;
    public float zoomSensitivity = 1.0f;
    
    private float defaultFOV;

    [SerializeField] private Camera assignedCamera;

    private Touch touchZero;
    private Touch touchOne;
    private Vector2 touchZeroPrevPos;
    private Vector2 touchOnePrevPos;
    private float prevMagnitude;
    private float currentMagnitude;
    private float difference;

    private void Start()
    {
        assignedCamera = transform.GetChild(0).GetComponent<Camera>();
        Deactivate();
        defaultFOV = assignedCamera.fieldOfView;
    }

    private void Update()
    {
        if (Input.touchCount >= 2)
        {
            touchZero = Input.GetTouch(0);
            touchOne = Input.GetTouch(1);

            touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            difference = currentMagnitude - prevMagnitude;
            zoom(difference * zoomSensitivity);
        }
    }

    void zoom(float increment)
    {
        assignedCamera.fieldOfView = Mathf.Clamp(assignedCamera.fieldOfView - increment, zoomOutMin, zoomOutMax);
    }

    public void Activate()
    {
        assignedCamera.gameObject.SetActive(true);
        assignedCamera.fieldOfView = defaultFOV;
    }

    public void Deactivate()
    {
        assignedCamera.gameObject.SetActive(false);
    }

    public void SetZoomSensitivity(float newSensitivity)
    {
        zoomSensitivity = newSensitivity;
    }

    public Camera returnAssignedCam()
    {
        return assignedCamera;
    }
}
