using UnityEngine;

public class Controls : MonoBehaviour
{
    public GameObject ControlsObject;
    public Joystick joystick;


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
