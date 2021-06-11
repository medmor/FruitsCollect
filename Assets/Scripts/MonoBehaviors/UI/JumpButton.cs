using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Image img;
    private Color originalColor;
    private void Start()
    {
        img = GetComponent<Image>();
        originalColor = img.color;

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        EventsManager.Instance.ControlsEvent.Invoke("Jump");
        img.color = Color.gray;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        img.color = originalColor;
    }
}