using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairAim : MonoBehaviour
{
    public static CrosshairAim instance;
    public LayerMask defaultMask;
    public LayerMask npcMask;
    public float crosshairSensitivity = 1f;

    private RectTransform crosshairRect;
    public RectTransform defaultPos;
    private Vector2 pos;
    private Camera currentCamera;
    private float resetTime = 0.5f;
    private Vector3 resetVelocity = Vector3.zero;
    private RaycastHit hit;
    private Ray ray;

    private Vector3 boxDir;
    private Vector3 boxStart;
    private Quaternion rotation;
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
        Gizmos.color = Color.red;
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
        GetHitRotation();
        //CheckForLockon();
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
        boxStart = currentCamera.ViewportToWorldPoint(crosshairRect.position);
        rotation = Quaternion.LookRotation(hit.point, Vector3.up);
        boxDir = rotation.eulerAngles;
    }

    public void CheckForLockon()
    {
        GetHitRotation();
        //Debug.Log("Current world space point" + currentCamera.ViewportToWorldPoint(crosshairRect.position));
        if (Physics.BoxCast(boxStart, boxSize, boxDir, out hit, rotation, Mathf.Infinity, npcMask))
        {
            Debug.Log("Hit NPC");
            Gizmos.DrawCube(boxStart + transform.forward * 50, transform.localScale);
        }
    }

    private void OnDrawGizmos()
    {
        bool isHit = Physics.BoxCast(boxStart, boxSize, transform.forward, out hit, transform.rotation, Mathf.Infinity, defaultMask);
        if (isHit)
        {
            Debug.Log("Hitting something");
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(boxStart + transform.forward * hit.distance, boxSize * 5);
        }
    }
}
