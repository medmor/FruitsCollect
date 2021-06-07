using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    public GameObject ControlsObject;
    public Button JumpButton;
    public Joystick joystick;

    public void Start()
    {

        JumpButton.onClick.AddListener(OnJumpButtonClick);
    }


    void OnJumpButtonClick()
    {
        EventsManager.Instance.ControlsEvent.Invoke("Jump");
    }

    public Joystick GetJoystick()
    {
        return joystick;
    }

    public void Hide()
    {
        ControlsObject.SetActive(false);
    }

    public void Show()
    {
        ControlsObject.SetActive(true);
    }
}
