using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Manager<GameManager>
{

    [HideInInspector]
    public readonly int NumberOfLevels = 9;
    public string currentLevelName { get; private set; } = "Boot";
    public Enums.GameState CurrentGameState { get; private set; } = Enums.GameState.PREGAME;

    //public Player PlayerPrefab;
    public GameObject[] SystemPrefabs;

    private void Start()
    {
        InstantiateSystemPrefabs();
    }

    void InstantiateSystemPrefabs()
    {
        for (int i = 0; i < SystemPrefabs.Length; ++i)
        {
            Instantiate(SystemPrefabs[i]);
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
    public void LoadLevel(string levelName, string levelNumber)
    {
        LoadLevel(levelName);
        currentLevelName += levelNumber;
    }

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (currentLevelName.StartsWith("Level"))
        {
            SaveManager.Instance.SetLevel(int.Parse(currentLevelName.Substring(5)));
            SoundManager.Instance.PlayMusic(0);
            Instantiate(Resources.Load("Levels/" + currentLevelName));
            GameObject.Find("/Player").transform.position =
                GameObject.Find(currentLevelName + "(Clone)/GamePoints/SpawnPoint").gameObject.transform.position;
            GameObject.Find("Cam/Vcam").GetComponent<CinemachineConfiner>().m_BoundingShape2D =
            GameObject.Find(currentLevelName + "(Clone)/WorldCamBounds").GetComponent<PolygonCollider2D>();
        }

        if (currentLevelName == "Boot")
        {
            SoundManager.Instance.StopMusic();
            SoundManager.Instance.playSound("click");
            UIManager.Instance.PlayerInventory.Hide();
            UIManager.Instance.PlayerInventory.Reset(true);
            UIManager.Instance.BootMenu.Show();
            UIManager.Instance.Controls.Hide();
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