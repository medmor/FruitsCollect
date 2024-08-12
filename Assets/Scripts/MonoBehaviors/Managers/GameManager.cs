using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Manager<GameManager>
{

    public GameMangerSO GameSettings;

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
        GameSettings.currentLevelName = levelName;
    }
    public void LoadLevel(string levelName, string levelNumber)
    {
        LoadLevel(levelName);
        GameSettings.currentLevelName += levelNumber;
    }

    void OnLoadOperationComplete(AsyncOperation ao)
    {
        if (GameSettings.currentLevelName.StartsWith("Level"))
        {
            SaveManager.Instance.SetLevel(int.Parse(GameSettings.currentLevelName.Substring(5)));
            SoundManager.Instance.PlayMusic(0);
            Instantiate(Resources.Load("Levels/" + GameSettings.currentLevelName));
            GameObject.Find("/Player").transform.position =
                GameObject.Find(GameSettings.currentLevelName + "(Clone)/GamePoints/SpawnPoint").gameObject.transform.position;
            GameObject.Find("Cam/Vcam").GetComponent<CinemachineConfiner>().m_BoundingShape2D =
            GameObject.Find(GameSettings.currentLevelName + "(Clone)/WorldCamBounds").GetComponent<PolygonCollider2D>();
        }

        if (GameSettings.currentLevelName == "Intro")
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
        GameSettings.CurrentGameState = state;

        switch (GameSettings.CurrentGameState)
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
        UpdateState(GameSettings.CurrentGameState == Enums.GameState.RUNNING
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