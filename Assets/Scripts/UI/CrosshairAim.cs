using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairAim : MonoBehaviour
{
    public static CrosshairAim instance;
    public LayerMask mask;
    public float crosshairSensitivity = 1f;

    private RectTransform crosshairRect;
    public RectTransform defaultPos;
    private Vector2 pos;
    private Camera currentCamera;
    private float resetTime = 0.5f;
    private Vector3 resetVelocity = Vector3.zero;
    
    [SerializeField] JoystickInput newLeftStick;

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

    private void Start()
    {
        currentCamera = PlayerManager.instance.GetCurrentCamera();
        crosshairRect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (newLeftStick.isDragging)
        {
            pos = crosshairRect.position;

            pos.x += newLeftStick.Horizontal * Time.deltaTime * crosshairSensitivity;
            pos.y += newLeftStick.Vertical * Time.deltaTime * crosshairSensitivity;

            crosshairRect.position = pos;
        }
        else if (!newLeftStick.isDragging)
        {
            crosshairRect.position = Vector3.SmoothDamp(crosshairRect.position, defaultPos.position, ref resetVelocity, resetTime);
        }
    }

    //shoots a raycast to tell where the crosshair is on
    public Vector3 CrosshairPointer()
    {
        RaycastHit hit;
        Ray ray = currentCamera.ScreenPointToRay(crosshairRect.position);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask) && hit.transform != null)
        {
            return hit.point;
        }
        else
        {
            return Vector3.forward;
        }
    }
}
