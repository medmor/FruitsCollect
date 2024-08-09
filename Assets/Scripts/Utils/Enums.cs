
public static class Enums
{
    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED,
        GAMEOVER
    }


    public enum SoundsEffects
    {
        JUPM,
        HIT,
        COIN,
        DIE
    }

    public enum Players
    {
        Player = 0,
        Player2 = 1,
    }

    public enum Axes
    {
        X,
        Y
    }

    public enum MovementType
    {
        AllDirections = 0,
        OnlyHorizontal,
        OnlyVertical
    }

    public enum Directions
    {
        Up,
        Right,
        Down,
        Left,
    }

    public enum KeyGroups
    {
        ArrowKeys,
        WASD,
    }

    public enum Targets
    {
        ThisObject,
        ObjectThatCollided,
    }


}