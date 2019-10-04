using UnityEngine;
using UnityEngine.Events;

public class Events
{
    [System.Serializable]public class LevelStateEvent : UnityEvent<LevelState, LevelState>{}
}
