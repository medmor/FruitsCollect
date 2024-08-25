using UnityEngine.Events;

public class EventsManager : Manager<EventsManager>
{
    public VoidEvent Playerkilled { get; private set; }
    public VoidEvent AllItemsCollected { get; private set; }
    public VoidEvent TimeOut { get; private set; }
    public IntEvent OnLevelChoosen { get; private set; }
    public VoidEvent OnBulletsAmountChanged { get; private set; }

    public override void Awake()
    {
        base.Awake();
        Playerkilled = new VoidEvent();
        AllItemsCollected = new VoidEvent();
        TimeOut = new VoidEvent();
        OnLevelChoosen = new IntEvent();
        OnBulletsAmountChanged = new VoidEvent();
    }

}

[System.Serializable] public class VoidEvent : UnityEvent { }
[System.Serializable] public class GameObjectEvent : UnityEvent<Player> { }
[System.Serializable] public class StringEvent : UnityEvent<string> { }
[System.Serializable] public class IntEvent : UnityEvent<int> { }
