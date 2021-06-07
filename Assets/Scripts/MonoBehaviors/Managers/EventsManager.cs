using UnityEngine.Events;

public class EventsManager : Manager<EventsManager>
{
    public GameObjectEvent Playerkilled { get; private set; }
    public VoidEvent AllItemsCollected { get; private set; }

    public StringEvent ControlsEvent { get; private set; }
    private void Start()
    {
        Playerkilled = new GameObjectEvent();
        AllItemsCollected = new VoidEvent();
        ControlsEvent = new StringEvent();

        Playerkilled.AddListener(GameManager.Instance.OnPlyerKilled);
    }

}

[System.Serializable] public class VoidEvent : UnityEvent { }
[System.Serializable] public class GameObjectEvent : UnityEvent<Player> { }
[System.Serializable] public class StringEvent : UnityEvent<string> { }
