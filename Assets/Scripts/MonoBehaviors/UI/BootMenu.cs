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

    [Header("Levels")]
    [SerializeField] private GameObject levelsContainer = default;
    [SerializeField] private GameObject levelPrefab = default;

    void Start()
    {

    }

    public void Hide()
    {
        mainMenu.SetActive(false);
        dummyCammera.gameObject.SetActive(false);
    }

    public void Show()
    {
        initLevels();
        mainMenu.SetActive(true);
        dummyCammera.gameObject.SetActive(true);
    }

    private void initLevels()
    {
        var lastLevel = SaveManager.Instance.GetLevel();
        foreach (Transform child in levelsContainer.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings - 3; i++)
        {
            var levelButton = Instantiate(levelPrefab).GetComponent<Button>();
            var text = levelButton.GetComponentInChildren<TextMeshProUGUI>();
            var lockImage = levelButton.transform.GetChild(1).gameObject.GetComponent<Image>();

            if (i + 1 <= lastLevel)
            {
                lockImage.gameObject.SetActive(false);
            }
            text.text = (i + 1).ToString();

            levelButton.transform.SetParent(levelsContainer.transform);

            levelButton.onClick.AddListener(() =>
            {
                if (int.Parse(text.text) <= lastLevel)
                {
                    SoundManager.Instance.playSound("click");
                    UIManager.Instance.BootMenu.Hide();
                    UIManager.Instance.PlayerInventory.Show();
                    UIManager.Instance.Controls.Show();
                    GameManager.Instance.LoadLevel("Level" + text.text);
                }
            });
        }
    }

}
