using UnityEngine;

[CreateAssetMenu(fileName = "GameMangerSO", menuName = "GameManger")]
public class GameMangerSO : ScriptableObject
{
    public int NumberOfLevels = 9;
    public string currentLevelName = "Boot";
    public Enums.GameState CurrentGameState = Enums.GameState.PREGAME;
}
