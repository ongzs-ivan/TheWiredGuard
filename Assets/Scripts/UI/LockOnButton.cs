using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LockOnButton : MonoBehaviour, IPointerClickHandler
{
    public static LockOnButton instance;

    public bool isLockedOn = false;

    public GameObject lockonCrosshair;
    public RectTransform defaultRect;
    private Camera currentCamera;
    private Image lockonIcon;
    private RectTransform lockonRect;

    private float resetTime = 0.5f;
    private Vector3 resetVelocity = Vector3.zero;

    private GameObject lockonTarget;

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

    private void LateUpdate()
    {
        if (lockonTarget != null)
        {
            lockonRect.position = currentCamera.WorldToScreenPoint(lockonTarget.transform.position);
            lockonIcon.color = Color.green;
        }
        else if (lockonTarget == null)
        {
            isLockedOn = false;
            lockonRect.position = Vector3.SmoothDamp(lockonRect.position, defaultRect.position, ref resetVelocity, resetTime);
            lockonIcon.color = Color.red;
        }
    }

    private void Start()
    {
        lockonIcon = lockonCrosshair.GetComponent<Image>();
        lockonRect = lockonCrosshair.GetComponent<RectTransform>();
        currentCamera = PlayerManager.instance.GetCurrentCamera();
    }

    public void CanLockOn(GameObject currentLockOnTarget)
    {
        lockonTarget = currentLockOnTarget;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isLockedOn)
            isLockedOn = false;
        else
            isLockedOn = true;
    }

    public GameObject returnLockOnTarget()
    {
        return lockonTarget;
    }

    public RectTransform returnLockOnRect()
    {
        return lockonRect;
    }
}
