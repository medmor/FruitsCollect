using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BootMenu : MonoBehaviour
{
    [Header("Dummy Camera")]
    [SerializeField] private Camera dummyCammera = default;

    [Header("Boot Menu")]
    [SerializeField] private GameObject mainMenu = default;
    [SerializeField] private Button play = default;

    [Header("Levels")]
    [SerializeField] private GameObject levelsContainer = default;
    [SerializeField] private GameObject levelPrefab = default;

    void Start()
    {
        play.onClick.AddListener(() => {
            GameManager.Instance.LoadLevel("Level1");
            Hide();
            UIManager.Instance.playerInventory.Show();
        });
        initLevels();
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

    private void initLevels()
    {
        for(int i = 0; i < SceneManager.sceneCountInBuildSettings - 3; i++)
        {
            var level = Instantiate(levelPrefab).GetComponent<Button>();
            var text = level.GetComponentInChildren<TextMeshProUGUI>();
            text.text = (i + 1).ToString();
            level.transform.SetParent(levelsContainer.transform);

            level.onClick.AddListener(() =>
            {
                SoundManager.Instance.PlayMusic(0);
                UIManager.Instance.bootMenu.Hide();
                UIManager.Instance.playerInventory.Show();
                GameManager.Instance.LoadLevel("Level" + text.text);
            });
        }
    }

}
