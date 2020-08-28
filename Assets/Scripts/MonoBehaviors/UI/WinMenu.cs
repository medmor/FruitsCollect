using UnityEngine;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    [Header("Dummy Camera")]
    [SerializeField] private Camera dummyCammera = default;

    [Header("Win Menu")]
    [SerializeField] private GameObject winMenu = default;
    [SerializeField] private Button restart = default;

    void Start()
    {
        restart.onClick.AddListener(() => {
            GameManager.Instance.LoadLevel("Boot");
            Hide();
            UIManager.Instance.bootMenu.Show();
        });
    }

    public void Hide()
    {
        winMenu.SetActive(false);
        dummyCammera.gameObject.SetActive(false);
    }

    public void Show()
    {
        winMenu.SetActive(true);
        dummyCammera.gameObject.SetActive(true);
    }
}
