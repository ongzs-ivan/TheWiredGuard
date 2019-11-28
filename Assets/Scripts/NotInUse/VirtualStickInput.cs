using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualStickInput : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private Image bgImg;
    private Image joystickImg;
    private Vector3 inputVector;
    private Vector3 initPos = Vector3.zero;
    private Vector3 initClickOffset = Vector3.zero;

    private void Start()
    {
        bgImg = GetComponent<Image>();
        joystickImg = transform.GetChild(0).GetComponent<Image>();
        initPos = (Vector3)joystickImg.rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        joystickImg.rectTransform.anchoredPosition = (Vector3)eventData.position - initClickOffset;

        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
            pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

            inputVector = new Vector3(pos.x * 2 + 1, 0, pos.y * 2 - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystickImg.rectTransform.anchoredPosition =
                new Vector3
                (inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3)
                ,inputVector.z * (bgImg.rectTransform.sizeDelta.y / 3)
                ,0
                );
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        initClickOffset = (Vector3)eventData.position - initPos;
        OnDrag(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        inputVector = Vector3.zero;
        joystickImg.rectTransform.anchoredPosition = initPos;
    }

    public float Horizontal()
    {
        if (inputVector.x != 0)
            return inputVector.x;
        else
            return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputVector.z != 0)
            return inputVector.z;
        else
            return Input.GetAxis("Vertical");
    }
}
