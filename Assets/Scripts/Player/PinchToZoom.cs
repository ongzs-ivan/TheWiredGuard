using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchToZoom : MonoBehaviour
{
    [SerializeField] private float decayRate = 0f;
    private float startDistance = 0f;
    private float pinchDelta = 0f;
    [SerializeField] private float zoomSensitivity = 0f;

    private void Update()
    {
        GetPinchDelta();
        Zoom();
    }

    private void Zoom()
    {
        this.transform.Translate(0, 0, pinchDelta * Time.deltaTime * zoomSensitivity, Space.Self);
        pinchDelta = Mathf.Lerp(pinchDelta, 0, decayRate * Time.deltaTime);
    }

    /// <summary>
    /// Gets pinch delta
    /// Compares two finger distances over time
    /// </summary>
    void GetPinchDelta()
    {
        if (Input.touchCount == 2 && Input.GetTouch(1).phase == TouchPhase.Began)
        {
            startDistance = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
        }

        // calculates current distance over time when either fingers have moved
        if (Input.touchCount == 2)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                float curDistance = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);

                pinchDelta = curDistance - startDistance;
                pinchDelta /= startDistance;
            }
        }
    }
}
