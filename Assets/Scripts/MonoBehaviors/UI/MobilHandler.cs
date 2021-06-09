using UnityEngine;

public class MobilHandler : MonoBehaviour
{
    public GameObject MobilUI;

    public void Hide()
    {
        MobilUI.gameObject.SetActive(false);
    }
    public void Show()
    {
        MobilUI.gameObject.SetActive(true);
    }
}
