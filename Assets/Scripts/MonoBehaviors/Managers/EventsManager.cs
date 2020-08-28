using UnityEngine;
using UnityEngine.Events;

public class EventsManager : Manager<EventsManager>
{
    public GameObjectEvent playerkilled { get; private set; }
    public VoidEvent allItemsCollected { get; private set; }

    private void Start()
    {
        playerkilled = new GameObjectEvent();
        allItemsCollected = new VoidEvent();

        playerkilled.AddListener(GameManager.Instance.OnPlyerKilled);
    }

}

[System.Serializable] public class VoidEvent : UnityEvent { }
[System.Serializable] public class GameObjectEvent : UnityEvent<Player> { }
