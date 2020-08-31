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

            if (i + 1 > lastLevel)
            {
                levelButton.GetComponent<Image>().color = new Color(1, 1, 1, .2f);
            }
            text.text = (i + 1).ToString();

            levelButton.transform.SetParent(levelsContainer.transform);

            levelButton.onClick.AddListener(() =>
            {
                if(int.Parse(text.text) <= lastLevel)
                {
                    SoundManager.Instance.PlayMusic(0);
                    UIManager.Instance.bootMenu.Hide();
                    UIManager.Instance.playerInventory.Show();
                    GameManager.Instance.LoadLevel("Level" + text.text);
                }
            });
        }
    }

}
