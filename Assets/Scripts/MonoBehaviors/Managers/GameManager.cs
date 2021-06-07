using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Manager<GameManager>
{
    public string currentLevelName { get; private set; } = "";
    public Enums.GameState CurrentGameState { get; private set; } = Enums.GameState.PREGAME;

    public Player PlayerPrefab;
    public GameObject[] SystemPrefabs;
    private List<GameObject> instancedSystemPrefabs;

    private void Start()
    {
        instancedSystemPrefabs = new List<GameObject>();
        InstantiateSystemPrefabs();
    }

    void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;
        for (int i = 0; i < SystemPrefabs.Length; ++i)
        {
            prefabInstance = Instantiate(SystemPrefabs[i]);
            instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    public void LoadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        if (ao == null)
        {
            Debug.LogError("[GameManager] Unable to load level " + levelName);
            return;
        }

        ao.completed += OnLoadOperationComplete;
        currentLevelName = levelName;
    }

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (currentLevelName.StartsWith("level"))
            SaveManager.Instance.SetLevel(int.Parse(currentLevelName.Substring(5)));
        if (currentLevelName == "Level1")
        {
            SoundManager.Instance.PlayMusic(0);
        }
    }

    public void UpdateState(Enums.GameState state)
    {
        CurrentGameState = state;

        switch (CurrentGameState)
        {
            case Enums.GameState.PREGAME:
                Time.timeScale = 1.0f;
                break;

            case Enums.GameState.RUNNING:
                Time.timeScale = 1.0f;
                break;

            case Enums.GameState.PAUSED:
                Time.timeScale = 0.0f;
                break;

            default:
                break;
        }
    }

    public void TogglePause()
    {
        UpdateState(CurrentGameState == Enums.GameState.RUNNING
            ? Enums.GameState.PAUSED : Enums.GameState.RUNNING);
    }

    public void RestartGame()
    {
        UpdateState(Enums.GameState.PREGAME);
    }

    public void OnPlyerKilled(Player player)
    {
        LoadLevel("GameOver");
        SoundManager.Instance.StopMusic();
        UIManager.Instance.PlayerInventory.Reset(true);
        UIManager.Instance.PlayerInventory.Hide();
        UIManager.Instance.Controls.Hide();
        UIManager.Instance.GameOverMenu.Show();
    }
}