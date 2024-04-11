using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private RectTransform joystickHandle;
    private RectTransform joystickBackground;
    private Vector2 inputDirection;

    private void Start()
    {
        joystickBackground = GetComponent<RectTransform>();
        joystickHandle = transform.GetChild(0).GetComponent<RectTransform>(); //  Ручка является дочерним объектом джойстика
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = new Vector2(eventData.position.x, eventData.position.y) - joystickBackground.anchoredPosition;

        inputDirection = (direction.magnitude > joystickBackground.sizeDelta.x / 2f) ? direction.normalized : direction / (joystickBackground.sizeDelta.x / 2f);
        joystickHandle.anchoredPosition = inputDirection * joystickBackground.sizeDelta.x / 2f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputDirection = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }

    public Vector2 GetDirection()
    {
        return inputDirection;
    }
}
