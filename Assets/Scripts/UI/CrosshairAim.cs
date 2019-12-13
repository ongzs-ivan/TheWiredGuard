using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairAim : MonoBehaviour
{
    public static CrosshairAim instance;
    public LayerMask defaultMask;
    public LayerMask npcMask;
    public float crosshairSensitivity = 500f;
    
    private RectTransform screensize;
    private RectTransform crosshairRect;
    public RectTransform defaultPos;
    private Vector2 pos;
    private Camera currentCamera;
    private Transform worldSpaceTransform;

    private float resetTime = 0.5f;
    private Vector3 resetVelocity = Vector3.zero;
    private RaycastHit hit;
    private RaycastHit boxHit;
    private bool isHit;
    private Ray ray;
    public Vector3 boxSize;

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
        boxSize = new Vector3(5, 5, 5);
        if (worldSpaceTransform == null)
            worldSpaceTransform = new GameObject("World Space Transform").transform;
        Gizmos.color = Color.red;
        currentCamera = PlayerManager.instance.GetCurrentCamera();
        screensize = transform.GetComponentInParent<RectTransform>();
        crosshairRect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (newLeftStick.isDragging && !LockOnButton.instance.isLockedOn)
        {
            pos = crosshairRect.position;

            pos.x += newLeftStick.Horizontal * Time.deltaTime * crosshairSensitivity;
            pos.y += newLeftStick.Vertical * Time.deltaTime * crosshairSensitivity;

            pos.x = Mathf.Clamp(pos.x, 0, Screen.width - screensize.sizeDelta.x);
            pos.y = Mathf.Clamp(pos.y, 0, Screen.height - screensize.sizeDelta.y);

            crosshairRect.position = pos;
        }
        else if (!newLeftStick.isDragging)
        {
            crosshairRect.position = Vector3.SmoothDamp(crosshairRect.position, defaultPos.position, ref resetVelocity, resetTime);
        }
        else if (LockOnButton.instance.isLockedOn)
        {
            crosshairRect.position = Vector3.SmoothDamp(crosshairRect.position, LockOnButton.instance.returnLockOnRect().position, ref resetVelocity, resetTime);
        }
        CheckForLockon();
    }

    //shoots a raycast to tell where the crosshair is on
    public Vector3 CrosshairPoint()
    {
        ray = currentCamera.ScreenPointToRay(crosshairRect.position);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, defaultMask) && hit.transform != null)
        {
            return hit.point;
        }
        else
        {
            return Vector3.forward;
        }
    }

    public GameObject CrosshairHit()
    {
        ray = currentCamera.ScreenPointToRay(crosshairRect.position);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, defaultMask) && hit.transform != null)
        {
            return hit.transform.gameObject;
        }
        else
        {
            return null;
        }
    }

    public void GetHitRotation()
    {
        if (worldSpaceTransform != null)
        {
            worldSpaceTransform.position = currentCamera.ViewportToWorldPoint(crosshairRect.position);
            worldSpaceTransform.rotation = Quaternion.LookRotation(ray.direction, Vector3.up);
        }
        
    }

    public void CheckForLockon()
    {
        GetHitRotation();
        isHit = (Physics.BoxCast(ray.origin, boxSize, ray.direction * 600, out boxHit, worldSpaceTransform.rotation, Mathf.Infinity, defaultMask));
        if (!isHit)
        {
            LockOnButton.instance.CanLockOn(null);
        }
        else if (isHit)
        {
            if (boxHit.transform.gameObject.layer == 8 && !LockOnButton.instance.isLockedOn)
                LockOnButton.instance.CanLockOn(boxHit.transform.gameObject);
        }
    }

    public void LockingOn()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        // drawing box ray cast
        Gizmos.DrawRay(ray.origin + new Vector3(boxSize.x, boxSize.y, boxSize.z)  , ray.direction * 1000);
        Gizmos.DrawRay(ray.origin + new Vector3(-boxSize.x, -boxSize.y, boxSize.z), ray.direction * 1000);
        Gizmos.DrawRay(ray.origin + new Vector3(+boxSize.x, -boxSize.y, boxSize.z), ray.direction * 1000);
        Gizmos.DrawRay(ray.origin + new Vector3(-boxSize.x, +boxSize.y, boxSize.z), ray.direction * 1000);
    }
}
