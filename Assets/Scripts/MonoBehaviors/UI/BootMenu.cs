using UnityEngine;
using UnityEngine.UI;

public class BootMenu : MonoBehaviour
{
    [Header("Dummy Camera")]
    [SerializeField] private Camera dummyCammera = default;

    [Header("Boot Menu")]
    [SerializeField] private GameObject mainMenu = default;
    [SerializeField] private Button play = default;

    void Start()
    {
        play.onClick.AddListener(() => {
            GameManager.Instance.LoadLevel("Level1");
            Hide();
            UIManager.Instance.playerInventory.Show();
        });
    }

    public void Hide()
    {
        mainMenu.SetActive(false);
        dummyCammera.gameObject.SetActive(false);
    }

    public void Show()
    {
        mainMenu.SetActive(true);
        dummyCammera.gameObject.SetActive(true);
    }

}
