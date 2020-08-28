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
        player.Reset();
        if(player.inventory.Hearts <= 0)
        {
            LoadLevel("GameOver");
            UIManager.Instance.playerInventory.Reset();
            UIManager.Instance.playerInventory.Hide();
            UIManager.Instance.gameOverMenu.Show();
        }
        //var newPlayer = Instantiate(PlayerPrefab);
        //newPlayer.spawnPoint = player.spawnPoint;
        //newPlayer.transform.position = newPlayer.spawnPoint.transform.position;
        //Destroy(player.gameObject);
        
    }
}