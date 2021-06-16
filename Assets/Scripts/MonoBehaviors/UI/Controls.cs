using UnityEngine;
using UnityEngine.UI;

public class Controls : MonoBehaviour
{
    public GameObject ControlsObject;
    public Joystick joystick;
    public GameObject JumpButton;
    public Button ControlsInfoShowButton;
    public Button ControlsInfoHideButton;
    public GameObject ControlsInfo;

    private void Start()
    {
        if (Platform.IsMobileBrowser())
        {
            SetMobileControls();
        }
        else
        {
            SetDescktopControls();
        }
    }
    public Joystick GetJoystick()
    {
        return joystick;
    }

    public void SetMobileControls()
    {
        joystick.gameObject.SetActive(true);
        JumpButton.SetActive(true);
    }

    public void SetDescktopControls()
    {
        ControlsInfoShowButton.gameObject.SetActive(true);
        ControlsInfoShowButton.onClick.AddListener(() => { ControlsInfo.SetActive(true); });
        ControlsInfoHideButton.onClick.AddListener(() => { ControlsInfo.SetActive(false); });
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
