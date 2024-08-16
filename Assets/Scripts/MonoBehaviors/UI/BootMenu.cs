﻿using TMPro;
using UnityEngine;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BootMenu : MonoBehaviour
{
    [Header("Dummy Camera")]
    [SerializeField] private Camera dummyCammera = default;

    [Header("IntroContainer")]
    [SerializeField] private GameObject introContainer = default;
    [SerializeField] private Button playButton = default;


    [Header("Boot Menu")]
    [SerializeField] private GameObject mainMenu = default;

    [Header("Levels")]
    [SerializeField] private GameObject levelsContainer = default;
    [SerializeField] private GameObject levelPrefab = default;


    private void Start()
    {
        playButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.playSound("click");
            introContainer.SetActive(false);
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

        for (int i = 0; i < GameManager.Instance.GameSettings.NumberOfLevels; i++)
        {
            var levelButton = Instantiate(levelPrefab).GetComponent<Button>();
            var text = levelButton.GetComponentInChildren<TextMeshProUGUI>();
            var lockImage = levelButton.transform.GetChild(1).gameObject.GetComponent<Image>();
            var level = i + 1;
            if (level <= lastLevel)
            {
                lockImage.gameObject.SetActive(false);
            }
            text.text = level.ToString();

            levelButton.transform.SetParent(levelsContainer.transform);

            levelButton.onClick.AddListener(() =>
            {
                if (level <= lastLevel)
                {
                    SoundManager.Instance.playSound("click");

                    EventsManager.Instance.OnLevelChoosen.Invoke(level);
                }
            });
        }
    }

}
