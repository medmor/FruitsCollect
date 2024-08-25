using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Manager<GameManager>
{

    public GameMangerSO GameSettings;

    public GameObject[] SystemPrefabs;

    private void Start()
    {
        InstantiateSystemPrefabs();

        EventsManager.Instance.Playerkilled.AddListener(OnPlyerKilled);
        EventsManager.Instance.OnLevelChoosen.AddListener(OnLevelChoosen);
    }

    void InstantiateSystemPrefabs()
    {
        for (int i = 0; i < SystemPrefabs.Length; ++i)
        {
            Instantiate(SystemPrefabs[i]);
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

    public void LoadIntro()
    {
        var operation = SceneManager.LoadSceneAsync("Intro");
        operation.completed += (AsyncOperation operation) =>
        {
            SoundManager.Instance.StopMusic();
            SoundManager.Instance.playSound("click");
            UIManager.Instance.PlayerInventory.Hide();
            UIManager.Instance.PlayerInventory.ResetInventory(true);
            UIManager.Instance.BootMenu.Show();
            UIManager.Instance.Controls.Hide();
        };
    }
    public void LoadWin()
    {
        UIManager.Instance.PlayerInventory.ResetInventory(true);
        SoundManager.Instance.StopMusic();
        SceneManager.LoadScene("Win");
        UIManager.Instance.WinMenu.Show();
        UIManager.Instance.PlayerInventory.Hide();
        UIManager.Instance.Controls.Hide();
    }
    void OnLevelChoosen(int levelName)
    {
        GameSettings.currentLevel = levelName;
        SaveManager.Instance.SetLevel(levelName);

        var operation = SceneManager.LoadSceneAsync("Level");

        operation.completed += (AsyncOperation operation) =>
        {
            SoundManager.Instance.PlayMusic(0);
            Instantiate(Resources.Load("Levels/Level" + GameSettings.currentLevel));
            GameObject.Find("/Player").transform.position =
            GameObject.Find("/Level" + GameSettings.currentLevel + "(Clone)/GamePoints/SpawnPoint").gameObject.transform.position;
            GameObject.Find("/Cam/Vcam").GetComponent<CinemachineConfiner>().m_BoundingShape2D =
            GameObject.Find("/Level" + GameSettings.currentLevel + "(Clone)/WorldCamBounds").GetComponent<PolygonCollider2D>();
            UIManager.Instance.BootMenu.Hide();
        };
    }

    void OnPlyerKilled()
    {
        var operation = SceneManager.LoadSceneAsync("GameOver");
        operation.completed += (AsyncOperation operation) =>
        {
            SoundManager.Instance.StopMusic();
            UIManager.Instance.PlayerInventory.ResetInventory(true);
            UIManager.Instance.PlayerInventory.Hide();
            UIManager.Instance.Controls.Hide();
            UIManager.Instance.GameOverMenu.Show();
        };
    }
}